using System;
using Core.EventSystem;
using Core.Infrastructure;
using Features.Game.Domain;
using Features.Game.Events;

namespace Features.Game.Adapters.Input
{
    public class GameEventHandler : IDisposable
    {
        private readonly GameService _gameService;

        public GameEventHandler(GameService gameService)
        {
            _gameService = gameService;
            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        public void EnableGameInput()
        {
            SubscribeToGameInputEvents();
        }

        public void DisableGameInput()
        {
            UnsubscribeFromGameInputEvents();
        }

        private void SubscribeToEvents()
        {
            SubscribeToGameInputEvents();
            SubscribeToPauseMenuEvents();
        }

        private void UnsubscribeFromEvents()
        {
            UnsubscribeFromGameInputEvents();
            UnsubscribeFromPauseMenuEvents();
        }

        private void SubscribeToGameInputEvents()
        {
            EventBus.Subscribe<ShootPerformedEvent>(OnShootPerformed);
            EventBus.Subscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Subscribe<DroneUpdateEvent>(OnDroneUpdate);
            EventBus.Subscribe<MovePerformedEvent>(OnMovePerformed);
            EventBus.Subscribe<MoveCancelledEvent>(OnMoveCancelled);
        }

        private void SubscribeToPauseMenuEvents()
        {
            EventBus.Subscribe<PausePerformedEvent>(OnPausePerformed);
            EventBus.Subscribe<ResumeButtonClickedEvent>(OnResumeButtonClicked);
            EventBus.Subscribe<MainMenuButtonClickedEvent>(OnMainMenuButtonClicked);
        }

        private void UnsubscribeFromGameInputEvents()
        {
            EventBus.Unsubscribe<ShootPerformedEvent>(OnShootPerformed);
            EventBus.Unsubscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Unsubscribe<DroneUpdateEvent>(OnDroneUpdate);
            EventBus.Unsubscribe<MovePerformedEvent>(OnMovePerformed);
            EventBus.Unsubscribe<MoveCancelledEvent>(OnMoveCancelled);
        }

        private void UnsubscribeFromPauseMenuEvents()
        {
            EventBus.Unsubscribe<PausePerformedEvent>(OnPausePerformed);
            EventBus.Unsubscribe<ResumeButtonClickedEvent>(OnResumeButtonClicked);
            EventBus.Unsubscribe<MainMenuButtonClickedEvent>(OnMainMenuButtonClicked);
        }

        private void OnShootPerformed(ShootPerformedEvent @event)
        {
            _gameService.OnShootPerformed();
        }

        private void OnLookPerformed(LookPerformedEvent @event)
        {
            _gameService.OnLookPerformed(@event);
        }

        private void OnDroneUpdate(DroneUpdateEvent @event)
        {
            _gameService.OnDroneUpdate(@event);
        }

        private void OnMovePerformed(MovePerformedEvent @event)
        {
            _gameService.OnMovePerformed(@event);
        }

        private void OnMoveCancelled(MoveCancelledEvent @event)
        {
            _gameService.OnMoveCancelled();
        }

        private void OnPausePerformed(PausePerformedEvent @event)
        {
            _gameService.OnPausePerformed();
        }

        private void OnResumeButtonClicked(ResumeButtonClickedEvent @event)
        {
            _gameService.OnResumeButtonClicked();
        }

        private void OnMainMenuButtonClicked(MainMenuButtonClickedEvent @event)
        {
            _gameService.OnMainMenuButtonClicked();
        }
    }
}