namespace Features.Game.Model
{
    public class GameModel
    {
        public DroneModel Drone { get; set; }

        public GameModel(DroneModel drone)
        {
            Drone = drone;
        }
    }
}