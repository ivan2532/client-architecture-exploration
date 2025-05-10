using Features.Game.Events;

namespace Features.Game.Domain
{
    public class Game
    {
        public readonly Drone Drone;
        public readonly MainCharacter MainCharacter;

        public Game(Drone drone, MainCharacter mainCharacter)
        {
            Drone = drone;
            MainCharacter = mainCharacter;
        }

        public void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            Drone.OnLookPerformed(lookPerformedEvent);
        }

        public void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            MainCharacter.OnMovePerformed(movePerformedEvent);
        }

        public void OnMoveCancelled(MoveCancelledEvent moveCancelledEvent)
        {
            MainCharacter.OnMoveCancelled(moveCancelledEvent);
        }
    }
}