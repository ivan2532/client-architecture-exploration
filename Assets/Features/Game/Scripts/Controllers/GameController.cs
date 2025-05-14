using System;
using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.Game.Events;
using Features.Game.Views;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Features.Game.Controllers
{
    [UsedImplicitly]
    public class GameController : Controller<GameView>, IDisposable
    {
        private readonly GameView _view;
        private readonly Models.Game _model;
        private readonly DroneController _drone;

        public GameController(GameView view, ControllerService controllerService) : base(view)
        {
            _view = view;
            _model = new Models.Game();
            _drone = controllerService.GetController<DroneController>(_view.Drone);

            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<ShootPerformedEvent>(OnShootPerformed);
            EventBus.Subscribe<PausePerformedEvent>(OnPausePerformed);
            EventBus.Subscribe<ResumeButtonClickedEvent>(OnResumeButtonClicked);
            EventBus.Subscribe<MainMenuButtonClickedEvent>(OnMainMenuButtonClicked);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<ShootPerformedEvent>(OnShootPerformed);
            EventBus.Unsubscribe<PausePerformedEvent>(OnPausePerformed);
            EventBus.Unsubscribe<ResumeButtonClickedEvent>(OnResumeButtonClicked);
            EventBus.Unsubscribe<MainMenuButtonClickedEvent>(OnMainMenuButtonClicked);
        }

        private void OnShootPerformed(ShootPerformedEvent shootPerformedEvent)
        {
            var shootResult = _drone.Shoot();
            _model.ProcessShot(shootResult);
            UpdateHudViewModel();
        }

        private void OnPausePerformed(PausePerformedEvent pausePerformedEvent)
        {
            _model.OnPausePerformed();

            UpdateInputViewModel();
            UpdateGameViewModel();
            UpdatePauseMenuViewModel();
        }

        private void OnResumeButtonClicked(ResumeButtonClickedEvent resumeButtonClickedEvent)
        {
            _model.OnResumeButtonClicked();

            UpdatePauseMenuViewModel();
            UpdateGameViewModel();
            UpdateInputViewModel();
        }

        private void OnMainMenuButtonClicked(MainMenuButtonClickedEvent mainMenuButtonClickedEvent)
        {
            _model.OnMainMenuButtonClicked();

            UpdateGameViewModel();
            LoadMainMenu();
        }

        private void UpdateGameViewModel()
        {
            _view.UpdateViewModel(_model.CreateViewModel());
        }

        private void UpdateHudViewModel()
        {
            _view.Hud.UpdateViewModel(_model.CreateHudViewModel());
        }

        private void UpdatePauseMenuViewModel()
        {
            _view.PauseMenu.UpdateViewModel(_model.CreatePauseMenuViewModel());
        }

        private void UpdateInputViewModel()
        {
            _view.Input.UpdateViewModel(_model.CreateInputViewModel());
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}