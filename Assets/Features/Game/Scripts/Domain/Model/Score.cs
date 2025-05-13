namespace Features.Game.Domain.Model
{
    public struct Score
    {
        public int Value { get; private set; }

        public void Increment()
        {
            Value++;
        }
    }
}