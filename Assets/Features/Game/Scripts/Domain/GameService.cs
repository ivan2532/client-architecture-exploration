using System.Collections;
using Core.Infrastructure;
using Features.Game.Configuration;
using Features.Game.Domain.Model;
using Features.Game.Events;
using Features.Game.Ports.Output;
using Features.MainMenu;
using Features.MainMenu.Domain;
using UnityEngine.SceneManagement;

namespace Features.Game.Domain
{
    public class GameService
    {
        private GameConfiguration _configuration;
        private GameModel _model;
        private IGamePresenter _presenter;

        private MainMenuService _mainMenuService;
        private CoroutineRunner _coroutineRunner;

        public void Initialize(
            GameConfiguration configuration,
            IGamePresenter presenter,
            MainMenuService mainMenuService,
            CoroutineRunner coroutineRunner
        )
        {
            _configuration = configuration;
            _presenter = presenter;
            _mainMenuService = mainMenuService;
            _coroutineRunner = coroutineRunner;
        }

        public IEnumerator Load()
        {
            SubscribeToEvents();
            yield return SceneManager.LoadSceneAsync("Game");

            _presenter.Initialize();
            InitializeModel();

            _presenter.HideCursor();
        }

        public void Unload()
        {
            UnsubscribeFromEvents();
        }

        private void InitializeModel()
        {
            _model = new GameModel(_configuration, _presenter.GetDroneStartingState());
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<ShootPerformedEvent>(OnShootPerformed);
            EventBus.Subscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Subscribe<DroneUpdateEvent>(OnDroneUpdate);

            EventBus.Subscribe<MovePerformedEvent>(OnMovePerformed);
            EventBus.Subscribe<MoveCancelledEvent>(OnMoveCancelled);

            EventBus.Subscribe<PausePerformedEvent>(OnPausePerformed);
            EventBus.Subscribe<ResumeButtonClickedEvent>(OnResumeButtonClicked);
            EventBus.Subscribe<MainMenuButtonClickedEvent>(OnMainMenuButtonClicked);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<ShootPerformedEvent>(OnShootPerformed);
            EventBus.Unsubscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Unsubscribe<DroneUpdateEvent>(OnDroneUpdate);

            EventBus.Unsubscribe<MovePerformedEvent>(OnMovePerformed);
            EventBus.Unsubscribe<MoveCancelledEvent>(OnMoveCancelled);

            EventBus.Unsubscribe<PausePerformedEvent>(OnPausePerformed);
            EventBus.Unsubscribe<ResumeButtonClickedEvent>(OnResumeButtonClicked);
            EventBus.Unsubscribe<MainMenuButtonClickedEvent>(OnMainMenuButtonClicked);
        }

        private void OnShootPerformed(ShootPerformedEvent shootPerformedEvent)
        {
            var raycastShootResult = _presenter.ShootRaycastFromDrone();
            var shootResult = _model.OnShootPerformed(raycastShootResult);
            if (shootResult.ScoreChanged) _presenter.UpdateScore(_model.Score);
        }

        private void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            _model.OnLookPerformed(lookPerformedEvent);
            _presenter.UpdateDrone(_model.Drone);
        }

        private void OnDroneUpdate(DroneUpdateEvent updateEvent)
        {
            _model.OnDroneUpdate(updateEvent);
            _presenter.UpdateDrone(_model.Drone);
        }

        private void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            _model.OnMovePerformed(movePerformedEvent);
            _presenter.UpdateMainCharacter(_model.MainCharacter);
        }

        private void OnMoveCancelled(MoveCancelledEvent moveCancelledEvent)
        {
            _model.OnMoveCancelled();
            _presenter.UpdateMainCharacter(_model.MainCharacter);
        }

        private void OnPausePerformed(PausePerformedEvent pausePerformedEvent)
        {
            _presenter.FreezeTime();
            _presenter.DisableInput();
            _presenter.ShowPauseMenu();
            _presenter.ShowCursor();
        }

        private void OnResumeButtonClicked(ResumeButtonClickedEvent resumeButtonClickedEvent)
        {
            _presenter.HideCursor();
            _presenter.HidePauseMenu();
            _presenter.EnableInput();
            _presenter.ResumeTime();
        }

        private void OnMainMenuButtonClicked(MainMenuButtonClickedEvent mainMenuButtonClickedEvent)
        {
            _presenter.ResumeTime();
            Unload();
            _coroutineRunner.Run(_mainMenuService.Load());
        }
    }
}