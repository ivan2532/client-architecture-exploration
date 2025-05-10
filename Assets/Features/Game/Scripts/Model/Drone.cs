using System;
using Features.Game.Configuration;
using Features.Game.Events;

namespace Features.Game.Model
{
    public class DroneModel
    {
        public float Pitch { get; private set; }
        public float Yaw { get; private set; }

        private readonly DroneConfiguration _configuration;

        public DroneModel(DroneConfiguration configuration, float pitch, float yaw)
        {
            _configuration = configuration;

            Pitch = pitch;
            Yaw = yaw;
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