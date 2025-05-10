namespace Features.Game.Domain
{
    public class Score
    {
        public int Value { get; private set; }

        public void Increment()
        {
            Value++;
        }
    }
}