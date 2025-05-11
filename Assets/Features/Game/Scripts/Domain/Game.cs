using Features.Game.Views;

namespace Features.Game.Domain
{
    public class Game
    {
        public readonly Score Score = new();

        public bool Paused { get; private set; }

        public ShootResult OnShootPerformed(RaycastShootResult raycastShootResult)
        {
            if (raycastShootResult.DummyTargetHit)
            {
                Score.Increment();
                return new ShootResult(true);
            }

            return new ShootResult(false);
        }

        public void OnPausePerformed()
        {
            Paused = true;
        }

        public void OnResumeButtonClicked()
        {
            Paused = false;
        }

        public void OnMainMenuButtonClicked()
        {
            Paused = false;
        }
    }
}