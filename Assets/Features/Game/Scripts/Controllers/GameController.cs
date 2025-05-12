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
            EventBus.Subscribe<PausePerformedEvent>(OnPausePerformed);
            EventBus.Subscribe<ResumeButtonClickedEvent>(OnResumeButtonClicked);
            EventBus.Subscribe<MainMenuButtonClickedEvent>(OnMainMenuButtonClicked);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<PausePerformedEvent>(OnPausePerformed);
            EventBus.Unsubscribe<ResumeButtonClickedEvent>(OnResumeButtonClicked);
            EventBus.Unsubscribe<MainMenuButtonClickedEvent>(OnMainMenuButtonClicked);
        }

        public ShootResult OnShootPerformed(ShootPerformedEvent shootPerformedEvent)
        {
            // if (shootPerformedEvent.RaycastShootResult.DummyTargetHit)
            // {
            //     Score.Increment();
            //     return new ShootResult(true);
            // }
            //
            return new ShootResult(false);
        }

        private void OnPausePerformed(PausePerformedEvent pausePerformedEvent)
        {
            _model.Paused = true;
        }

        private void OnResumeButtonClicked(ResumeButtonClickedEvent resumeButtonClickedEvent)
        {
            _model.Paused = false;
        }

        private void OnMainMenuButtonClicked(MainMenuButtonClickedEvent mainMenuButtonClickedEvent)
        {
            _model.Paused = false;
        }
    }
}