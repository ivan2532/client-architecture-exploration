using Features.Game.Events;

namespace Features.Game.Domain
{
    public class Game
    {
        public readonly Drone Drone;
        public readonly MainCharacter MainCharacter;

        public Score Score { get; private set; }
        public bool Paused { get; private set; }

        public Game(Drone drone, MainCharacter mainCharacter)
        {
            Drone = drone;
            MainCharacter = mainCharacter;
            Score = new Score();
        }

        public ShootResult OnShootPerformed(ShootPerformedEvent shootPerformedEvent)
        {
            // if (shootPerformedEvent.RaycastShootResult.DummyTargetHit)
            // {
            //     Score.Increment();
            //     return new ShootResult(true);
            // }
            //
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

        public void OnMoveCancelled()
        {
            MainCharacter.OnMoveCancelled();
        }

        public void OnPausePerformed()
        {
            Paused = true;
        }

        public void OnResumeButtonClicked()
        {
            Paused = false;
        }

        public void OnMainMenuButtonClicked()
        {
            Paused = false;
        }
    }
}