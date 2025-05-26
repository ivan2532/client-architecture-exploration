using System.Collections;
using Features.Game.Configuration;
using Features.Game.Domain.Model;
using Features.Game.Events;
using Features.Game.Ports.Output;
using Features.MainMenu.Domain;
using Utility;

namespace Features.Game.Domain
{
    public class GameService
    {
        private readonly GameConfiguration _configuration;
        private readonly IGamePresenter _presenter;
        private readonly IGameInputController _inputController;

        private GameModel _model;
        private MainMenuService _mainMenuService;

        public GameService(
            GameConfiguration configuration,
            IGamePresenter presenter,
            IGameInputController gameInputController
        )
        {
            _configuration = configuration;
            _presenter = presenter;
            _inputController = gameInputController;
        }

        public void ResolveMainMenuService(MainMenuService mainMenuService)
        {
            _mainMenuService = mainMenuService;
        }

        public IEnumerator LoadGameScene()
        {
            yield return _presenter.LoadGameScene();

            _presenter.Initialize();
            InitializeModel();

            _presenter.HideCursor();
        }

        public void OnShootPerformed()
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

        public void OnMoveCancelled()
        {
            _model.OnMoveCancelled();
            _presenter.UpdateMainCharacter(_model.MainCharacter);
        }

        public void OnPausePerformed()
        {
            var pauseResult = _model.OnPausePerformed();

            if (pauseResult.Paused)
            {
                _inputController.DisableInput();
                _presenter.PauseGame();
                _presenter.ShowCursor();
                _presenter.ShowPauseMenu();
            }
            else
            {
                _inputController.EnableInput();
                _presenter.HidePauseMenu();
                _presenter.HideCursor();
                _presenter.ResumeGame();
            }
        }

        public void OnResumeButtonClicked()
        {
            _model.OnResumeButtonClicked();

            _inputController.EnableInput();
            _presenter.HidePauseMenu();
            _presenter.HideCursor();
            _presenter.ResumeGame();
        }

        public void OnMainMenuButtonClicked()
        {
            _presenter.ShowCursor();
            _presenter.ResumeGame();
            _inputController.EnableInput();
            CoroutineRunner.Run(_mainMenuService.LoadMainMenuScene());
        }

        private void InitializeModel()
        {
            _model = new GameModel(_configuration, _presenter.GetDroneStartingState());
        }
    }
}