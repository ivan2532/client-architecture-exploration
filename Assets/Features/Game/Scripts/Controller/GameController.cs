using System;
using Core.Controller;
using Core.Infrastructure;
using Features.Game.Configuration;
using Features.Game.Domain;
using Features.Game.Events;
using Features.Game.Mappers;
using Features.Game.View;
using JetBrains.Annotations;

namespace Features.Game.Controller
{
    [UsedImplicitly]
    public class GameController : ControllerBase<GameView>, IDisposable
    {
        private readonly GameView _view;
        private readonly Domain.Game _game;

        public GameController(GameView view, GameConfiguration configuration) : base(view)
        {
            _view = view;
            _game = CreateModel(configuration, view);
            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        private Domain.Game CreateModel(GameConfiguration configuration, GameView view)
        {
            var drone = new Drone(
                configuration.Drone,
                view.DroneOffsetFromMainCharacter,
                view.DronePosition,
                view.DronePitch,
                view.DroneYaw
            );

            var mainCharacter = new MainCharacter(configuration.MainCharacter);

            return new Domain.Game(drone, mainCharacter);
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Subscribe<MovePerformedEvent>(OnMovePerformed);
            EventBus.Subscribe<MoveCancelledEvent>(OnMoveCancelled);
            EventBus.Subscribe<DroneUpdateEvent>(OnDroneUpdate);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Unsubscribe<MovePerformedEvent>(OnMovePerformed);
            EventBus.Unsubscribe<MoveCancelledEvent>(OnMoveCancelled);
            EventBus.Unsubscribe<DroneUpdateEvent>(OnDroneUpdate);
        }

        private void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            _game.OnLookPerformed(lookPerformedEvent);
            UpdateViewModel();
        }

        private void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            _game.OnMovePerformed(movePerformedEvent);
            UpdateViewModel();
        }

        private void OnMoveCancelled(MoveCancelledEvent moveCancelledEvent)
        {
            _game.OnMoveCancelled(moveCancelledEvent);
            UpdateViewModel();
        }

        private void OnDroneUpdate(DroneUpdateEvent updateEvent)
        {
            _game.OnDroneUpdate(updateEvent);
            UpdateViewModel();
        }

        private void UpdateViewModel()
        {
            _view.UpdateViewModel(GameToViewModelMapper.Map(_game));
        }
    }
}