namespace Features.Game.Model
{
    public struct Velocity
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Velocity(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Velocity Zero => new(0f, 0f, 0f);
    }
}