using System;
using Features.Game.Configuration;
using Features.Game.Events;
using Features.Game.ViewModels;
using UnityEngine;

namespace Features.Game.Models
{
    public class Drone
    {
        private readonly DroneConfiguration _configuration;
        private readonly Vector3 _offsetFromMainCharacter;

        private Vector3 _position;
        private float _pitch;
        private float _yaw;
        private Vector3 _velocity;

        public Drone(
            DroneConfiguration configuration,
            float pitch,
            float yaw,
            Vector3 offsetFromMainCharacter)
        {
            _configuration = configuration;
            _pitch = pitch;
            _yaw = yaw;
            _offsetFromMainCharacter = offsetFromMainCharacter;
        }

        public void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            _pitch = Math.Clamp(
                _pitch + lookPerformedEvent.InputDelta.X * _configuration.LookSensitivity,
                _configuration.MinimumPitch,
                _configuration.MaximumPitch);

            _yaw = Math.Clamp(
                _yaw - lookPerformedEvent.InputDelta.Y * _configuration.LookSensitivity,
                _configuration.MinimumYaw,
                _configuration.MaximumYaw);
        }

        public void OnUpdate(DroneUpdateEvent updateEvent)
        {
            _position = Vector3.SmoothDamp(
                updateEvent.DronePosition,
                updateEvent.MainCharacterPosition + _offsetFromMainCharacter,
                ref _velocity,
                _configuration.FollowSmoothTime
            );
        }

        public DroneViewModel CreateViewModel()
        {
            return new DroneViewModel(_position, _pitch, _yaw);
        }
    }
}