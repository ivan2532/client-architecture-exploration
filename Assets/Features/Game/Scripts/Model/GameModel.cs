using Features.Game.Events;

namespace Features.Game.Model
{
    public class GameModel
    {
        public readonly DroneModel Drone;
        public readonly MainCharacterModel MainCharacter;

        public GameModel(DroneModel drone, MainCharacterModel mainCharacter)
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