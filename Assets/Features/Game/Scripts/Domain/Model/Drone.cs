using System;
using Features.Game.Configuration;
using Features.Game.Events;
using UnityEngine;

namespace Features.Game.Domain.Model
{
    public class Drone
    {
        public Vector3 Position { get; private set; }
        public float Pitch { get; private set; }
        public float Yaw { get; private set; }

        private readonly DroneConfiguration _configuration;
        private readonly Vector3 _offsetFromMainCharacter;

        private Vector3 _velocity;

        public Drone(DroneConfiguration configuration, DroneStartingState droneStartingState)
        {
            _configuration = configuration;
            _offsetFromMainCharacter = droneStartingState.OffsetFromMainCharacter;
            Position = droneStartingState.Position;
            Pitch = droneStartingState.Pitch;
            Yaw = droneStartingState.Yaw;
        }

        public void OnUpdate(DroneUpdateEvent updateEvent)
        {
            Position = Vector3.SmoothDamp(
                updateEvent.DronePosition,
                updateEvent.MainCharacterPosition + _offsetFromMainCharacter,
                ref _velocity,
                _configuration.FollowSmoothTime
            );
        }

        public void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            Pitch = Math.Clamp(
                Pitch + lookPerformedEvent.InputDelta.X * _configuration.LookSensitivity,
                _configuration.MinimumPitch,
                _configuration.MaximumPitch);

            Yaw = Math.Clamp(
                Yaw - lookPerformedEvent.InputDelta.Y * _configuration.LookSensitivity,
                _configuration.MinimumYaw,
                _configuration.MaximumYaw);
        }
    }
}