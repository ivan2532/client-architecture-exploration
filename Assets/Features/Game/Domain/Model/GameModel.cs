using Features.Game.Configuration;
using Features.Game.Events;
using Features.Game.View.Model;
using UnityEngine;

namespace Features.Game.Domain.Model
{
    public class GameModel
    {
        public readonly Drone Drone;
        public readonly MainCharacter MainCharacter;

        public Score Score;

        public GameModel(
            GameConfiguration configuration,
            DroneStartingState droneStartingState)
        {
            Drone = new Drone(configuration.Drone, droneStartingState);
            MainCharacter = new MainCharacter(configuration.MainCharacter);
        }

        public ShootResult OnShootPerformed(RaycastShootResult raycastShootResult)
        {
            if (raycastShootResult.DummyTargetHit)
            {
                Score.Increment();
                return new ShootResult(true);
            }

            return new ShootResult(false);
        }

        public void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            Drone.OnLookPerformed(lookPerformedEvent);
        }

        public void OnDroneUpdate(DroneUpdateEvent droneUpdateEvent)
        {
            Drone.OnUpdate(droneUpdateEvent);
        }

        public void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            MainCharacter.OnMovePerformed(movePerformedEvent);
        }

        public void OnMoveCancelled()
        {
            MainCharacter.OnMoveCancelled();
        }
    }
}