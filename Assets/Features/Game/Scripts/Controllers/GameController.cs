using System;
using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.Game.Configuration;
using Features.Game.Events;
using Features.Game.Models;
using Features.Game.Views;
using JetBrains.Annotations;

namespace Features.Game.Controllers
{
    [UsedImplicitly]
    public class GameController : Controller<GameView>, IDisposable
    {
        private readonly GameView _view;
        private readonly GameConfiguration _configuration;

        private GameModel _model;

        public GameController(GameView view, GameConfiguration configuration) : base(view)
        {
            _view = view;
            _configuration = configuration;
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
            var raycastShootResult = _view.Drone.ShootRaycast();

            if (raycastShootResult.DummyTargetHit)
            {
                _model.Score.Increment();
                UpdateHudViewModel();
            }
        }

        private void OnPausePerformed(PausePerformedEvent pausePerformedEvent)
        {
            _model.Paused = true;
            UpdateInputViewModel();
            UpdateGameViewModel();
            UpdatePauseMenuViewModel();
        }

        private void OnResumeButtonClicked(ResumeButtonClickedEvent resumeButtonClickedEvent)
        {
            _model.Paused = false;
            UpdatePauseMenuViewModel();
            UpdateGameViewModel();
            UpdateInputViewModel();
        }

        private void OnMainMenuButtonClicked(MainMenuButtonClickedEvent mainMenuButtonClickedEvent)
        {
            _model.Paused = false;
            UpdateGameViewModel();
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
    }
}