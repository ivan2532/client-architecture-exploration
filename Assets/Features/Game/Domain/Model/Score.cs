namespace Features.Game.Domain.Model
{
    public struct Score
    {
        public int Value { get; private set; }

        public static Score Zero => new() { Value = 0 };

        public void Increment()
        {
            Value++;
        }
    }
}