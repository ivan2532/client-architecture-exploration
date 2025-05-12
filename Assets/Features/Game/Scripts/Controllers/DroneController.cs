using System;
using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.Game.Configuration;
using Features.Game.Events;
using Features.Game.Models;
using Features.Game.Views;
using JetBrains.Annotations;
using UnityEngine;

namespace Features.Game.Controllers
{
    [UsedImplicitly]
    public class DroneController : Controller<DroneView>, IDisposable
    {
        private readonly DroneConfiguration _configuration;
        private readonly Vector3 _offsetFromMainCharacter;

        private DroneModel _model;

        private Vector3 _velocity;

        public DroneController(DroneView view, DroneConfiguration configuration) : base(view)
        {
            _configuration = configuration;
            _offsetFromMainCharacter = view.OffsetFromMainCharacter;

            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Subscribe<DroneUpdateEvent>(OnUpdate);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Unsubscribe<DroneUpdateEvent>(OnUpdate);
        }

        private void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            _model.Pitch = Math.Clamp(
                _model.Pitch + lookPerformedEvent.InputDelta.X * _configuration.LookSensitivity,
                _configuration.MinimumPitch,
                _configuration.MaximumPitch);

            _model.Yaw = Math.Clamp(
                _model.Yaw - lookPerformedEvent.InputDelta.Y * _configuration.LookSensitivity,
                _configuration.MinimumYaw,
                _configuration.MaximumYaw);
        }

        private void OnUpdate(DroneUpdateEvent updateEvent)
        {
            _model.Position = Vector3.SmoothDamp(
                updateEvent.DronePosition,
                updateEvent.MainCharacterPosition + _offsetFromMainCharacter,
                ref _velocity,
                _configuration.FollowSmoothTime
            );
        }
    }
}