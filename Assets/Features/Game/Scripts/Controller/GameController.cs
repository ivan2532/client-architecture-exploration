using System;
using Core.Controller;
using Core.Infrastructure;
using Features.Game.Events;
using Features.Game.Mappers;
using Features.Game.Model;
using Features.Game.View;
using JetBrains.Annotations;
using UnityEngine;

namespace Features.Game.Controller
{
    // TODO: UsedImplicitly should be available in the latest JetBrains.Annotations
    // TODO: NuGet packages in Unity
    [UsedImplicitly]
    public class GameController : ControllerBase<GameView>, IDisposable
    {
        private readonly GameView _view;
        private readonly GameModel _game;

        public GameController(GameView view) : base(view)
        {
            _view = view;

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
            // TODO: Inject
            var lookSensitivity = 0.1f;
            var pitchRangeX = -45f;
            var pitchRangeY = 45f;
            var yawRangeX = -45f;
            var yawRangeY = 45f;

            _game.Drone.Pitch = Mathf.Clamp(
                _game.Drone.Pitch - lookPerformedEvent.InputDelta.Y * lookSensitivity,
                pitchRangeX,
                pitchRangeY);

            _game.Drone.Yaw = Mathf.Clamp(
                _game.Drone.Yaw + lookPerformedEvent.InputDelta.X * lookSensitivity,
                yawRangeX,
                yawRangeY);

            _view.UpdateViewModel(GameModelToViewModelMapper.Map(_game));
        }
    }
}