using Core.Infrastructure;
using Features.Game.Domain;
using Features.Game.Events;

namespace Features.Game.Adapters.Input
{
    public class GameEventHandler
    {
        private readonly GameService _gameService;

        public GameEventHandler(GameService gameService)
        {
            _gameService = gameService;
        }

        public void Enable()
        {
            SubscribeToEvents();
        }

        public void Disable()
        {
            UnsubscribeFromEvents();
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