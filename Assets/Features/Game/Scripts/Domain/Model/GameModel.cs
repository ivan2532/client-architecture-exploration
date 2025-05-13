using Features.Game.Configuration;
using Features.Game.Events;
using Features.Game.Views;
using UnityEngine;

namespace Features.Game.Domain.Model
{
    public class GameModel
    {
        private readonly Drone _drone;
        private readonly MainCharacter _mainCharacter;

        private Score _score;
        private bool _showCursor;
        private bool _paused;

        public GameModel(
            GameConfiguration configuration,
            Vector3 droneOffsetFromMainCharacter,
            Vector3 dronePosition,
            float dronePitch,
            float droneYaw)
        {
            _drone = new Drone(
                configuration.Drone,
                droneOffsetFromMainCharacter,
                dronePosition,
                dronePitch,
                droneYaw
            );

            _mainCharacter = new MainCharacter(configuration.MainCharacter);
        }

        public ShootResult OnShootPerformed(RaycastShootResult raycastShootResult)
        {
            if (raycastShootResult.DummyTargetHit)
            {
                _score.Increment();
                return new ShootResult(true);
            }

            return new ShootResult(false);
        }

        public void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            _drone.OnLookPerformed(lookPerformedEvent);
        }

        public void OnDroneUpdate(DroneUpdateEvent droneUpdateEvent)
        {
            _drone.OnUpdate(droneUpdateEvent);
        }

        public void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            _mainCharacter.OnMovePerformed(movePerformedEvent);
        }

        public void OnMoveCancelled()
        {
            _mainCharacter.OnMoveCancelled();
        }

        public void OnPausePerformed()
        {
            _showCursor = true;
            _paused = true;
        }

        public void OnResumeButtonClicked()
        {
            _showCursor = false;
            _paused = false;
        }

        public void OnMainMenuButtonClicked()
        {
            _showCursor = true;
            _paused = false;
        }
    }
}