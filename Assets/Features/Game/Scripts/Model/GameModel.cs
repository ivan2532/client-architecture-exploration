using Features.Game.Events;

namespace Features.Game.Model
{
    public class GameModel
    {
        public readonly DroneModel Drone;

        public GameModel(DroneModel drone)
        {
            Drone = drone;
        }

        public void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            Drone.OnLookPerformed(lookPerformedEvent);
        }
    }
}