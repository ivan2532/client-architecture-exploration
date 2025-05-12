using Features.Game.ViewModels;
using Features.Game.Views;

namespace Features.Game.Models
{
    public class Game
    {
        private Score _score;
        private bool _showCursor;
        private bool _paused;

        public ShootResult Shoot(RaycastShootResult raycastShootResult)
        {
            if (raycastShootResult.DummyTargetHit)
            {
                _score.Increment();
                return new ShootResult(true);
            }

            return new ShootResult(false);
        }

        public void OnPausePerformed()
        {
            _showCursor = true;
            _paused = true;
        }

        public void OnResumeButtonClicked()
        {
            _showCursor = false;
            _paused = false;
        }

        public void OnMainMenuButtonClicked()
        {
            _showCursor = true;
            _paused = false;
        }

        public GameViewModel CreateViewModel()
        {
            return new GameViewModel(_showCursor, !_paused, _paused ? 0f : 1f);
        }

        public HudViewModel CreateHudViewModel()
        {
            return new HudViewModel(_score.Value);
        }

        public PauseMenuViewModel CreatePauseMenuViewModel()
        {
            return new PauseMenuViewModel(_paused);
        }

        public InputViewModel CreateInputViewModel()
        {
            return new InputViewModel(!_paused);
        }
    }
}