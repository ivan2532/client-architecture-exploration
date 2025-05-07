namespace Features.Game.Model
{
    public class DroneModel
    {
        public float Pitch { get; set; }
        public float Yaw { get; set; }

        public DroneModel(float pitch, float yaw)
        {
            Pitch = pitch;
            Yaw = yaw;
        }
    }
}