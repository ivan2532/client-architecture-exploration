using System;
using Core.Controller;
using Core.Infrastructure;
using Features.Game.Configuration;
using Features.Game.Events;
using Features.Game.Mappers;
using Features.Game.Model;
using Features.Game.View;
using JetBrains.Annotations;
using UnityEngine;

namespace Features.Game.Controller
{
    [UsedImplicitly]
    public class GameController : ControllerBase<GameView>, IDisposable
    {
        private readonly GameView _view;
        private readonly GameConfiguration _configuration;
        private readonly GameModel _game;

        public GameController(GameView view, GameConfiguration configuration) : base(view)
        {
            _view = view;
            _configuration = configuration;

            var drone = new DroneModel(view.DronePitch, view.DroneYaw);
            _game = new GameModel(drone);

            EventBus.Subscribe<LookPerformedEvent>(OnLookPerformed);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<LookPerformedEvent>(OnLookPerformed);
        }

        private void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            _game.Drone.Pitch = Mathf.Clamp(
                _game.Drone.Pitch - lookPerformedEvent.InputDelta.Y * _configuration.LookSensitivity,
                _configuration.MinimumPitch,
                _configuration.MaximumPitch);

            _game.Drone.Yaw = Mathf.Clamp(
                _game.Drone.Yaw + lookPerformedEvent.InputDelta.X * _configuration.LookSensitivity,
                _configuration.MinimumYaw,
                _configuration.MaximumYaw);

            _view.UpdateViewModel(GameModelToViewModelMapper.Map(_game));
        }
    }
}