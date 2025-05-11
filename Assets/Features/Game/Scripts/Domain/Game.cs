using Features.Game.Events;
using Features.Game.View;

namespace Features.Game.Domain
{
    public class Game
    {
        public readonly Drone Drone;
        public readonly MainCharacter MainCharacter;
        public readonly Score Score;

        public bool Paused { get; private set; }

        public Game(Drone drone, MainCharacter mainCharacter)
        {
            Drone = drone;
            MainCharacter = mainCharacter;
            Score = new Score();
        }

        public ShootResult OnShootPerformed(ShootPerformedEvent shootPerformedEvent)
        {
            if (shootPerformedEvent.RaycastShootResult.DummyTargetHit)
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

        public void OnDroneUpdate(DroneUpdateEvent updateEvent)
        {
            Drone.Update(updateEvent);
        }

        public void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            MainCharacter.OnMovePerformed(movePerformedEvent);
        }

        public void OnMoveCancelled(MoveCancelledEvent moveCancelledEvent)
        {
            MainCharacter.OnMoveCancelled(moveCancelledEvent);
        }

        public void OnPausePerformed(PausePerformedEvent pausePerformedEvent)
        {
            Paused = true;
        }
    }
}