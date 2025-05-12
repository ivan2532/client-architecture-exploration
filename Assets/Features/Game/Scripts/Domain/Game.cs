using Features.Game.Views;

namespace Features.Game.Domain
{
    public class Game
    {
        public readonly Score Score = new();

        public bool ShowCursor { get; private set; }
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
            ShowCursor = true;
            Paused = true;
        }

        public void OnResumeButtonClicked()
        {
            ShowCursor = false;
            Paused = false;
        }

        public void OnMainMenuButtonClicked()
        {
            ShowCursor = true;
            Paused = false;
        }
    }
}