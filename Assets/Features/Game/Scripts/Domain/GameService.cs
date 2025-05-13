using System.Collections;
using Core.Infrastructure;
using Features.Game.Configuration;
using Features.Game.Domain.Model;
using Features.Game.Events;
using Features.Game.Ports.Input;
using Features.Game.Ports.Output;
using Features.MainMenu.Domain;
using UnityEngine.SceneManagement;

namespace Features.Game.Domain
{
    public class GameService
    {
        private GameConfiguration _configuration;
        private GameModel _model;
        private IGameEventHandler _eventHandler;
        private IGamePresenter _presenter;

        private MainMenuService _mainMenuService;
        private CoroutineRunner _coroutineRunner;

        public void Initialize(
            GameConfiguration configuration,
            IGameEventHandler eventHandler,
            IGamePresenter presenter,
            MainMenuService mainMenuService,
            CoroutineRunner coroutineRunner
        )
        {
            _configuration = configuration;
            _eventHandler = eventHandler;
            _presenter = presenter;
            _mainMenuService = mainMenuService;
            _coroutineRunner = coroutineRunner;
        }

        public IEnumerator Load()
        {
            _eventHandler.Enable();
            yield return SceneManager.LoadSceneAsync("Game");

            _presenter.Initialize();
            InitializeModel();

            _presenter.HideCursor();
        }

        public void Unload()
        {
            _eventHandler.Disable();
        }

        public void OnShootPerformed(ShootPerformedEvent shootPerformedEvent)
        {
            var raycastShootResult = _presenter.ShootRaycastFromDrone();
            var shootResult = _model.OnShootPerformed(raycastShootResult);
            if (shootResult.ScoreChanged) _presenter.UpdateScore(_model.Score);
        }

        public void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            _model.OnLookPerformed(lookPerformedEvent);
            _presenter.UpdateDrone(_model.Drone);
        }

        public void OnDroneUpdate(DroneUpdateEvent updateEvent)
        {
            _model.OnDroneUpdate(updateEvent);
            _presenter.UpdateDrone(_model.Drone);
        }

        public void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            _model.OnMovePerformed(movePerformedEvent);
            _presenter.UpdateMainCharacter(_model.MainCharacter);
        }

        public void OnMoveCancelled(MoveCancelledEvent moveCancelledEvent)
        {
            _model.OnMoveCancelled();
            _presenter.UpdateMainCharacter(_model.MainCharacter);
        }

        public void OnPausePerformed(PausePerformedEvent pausePerformedEvent)
        {
            _presenter.FreezeTime();
            _presenter.DisableInput();
            _presenter.ShowPauseMenu();
            _presenter.ShowCursor();
        }

        public void OnResumeButtonClicked(ResumeButtonClickedEvent resumeButtonClickedEvent)
        {
            _presenter.HideCursor();
            _presenter.HidePauseMenu();
            _presenter.EnableInput();
            _presenter.ResumeTime();
        }

        public void OnMainMenuButtonClicked(MainMenuButtonClickedEvent mainMenuButtonClickedEvent)
        {
            _presenter.ResumeTime();
            Unload();
            _coroutineRunner.Run(_mainMenuService.Load());
        }

        private void InitializeModel()
        {
            _model = new GameModel(_configuration, _presenter.GetDroneStartingState());
        }
    }
}