namespace Features.Game.Domain.Model
{
    public class Score
    {
        public int Value { get; private set; }

        public static Score Zero => new() { Value = 0 };

        private Score()
        {
        }

        public void Increment()
        {
            Value++;
        }
    }
}