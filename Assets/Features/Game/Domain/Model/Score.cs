namespace Features.Game.Domain.Model
{
    public class Score
    {
        public int Value { get; private set; }

        public static Score Zero => new(0);

        private Score(int value)
        {
            Value = value;
        }

        public void Increment()
        {
            Value++;
        }
    }
}