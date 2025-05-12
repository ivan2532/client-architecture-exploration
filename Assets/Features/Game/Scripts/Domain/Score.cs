namespace Features.Game.Domain
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