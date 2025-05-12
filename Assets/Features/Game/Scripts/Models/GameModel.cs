using Features.Game.ViewModels;

namespace Features.Game.Models
{
    public struct GameModel
    {
        public Score Score { get; set; }
        public bool ShowCursor { get; set; }
        public bool Paused { get; set; }

        public GameViewModel CreateViewModel()
        {
            return new GameViewModel(ShowCursor, !Paused, Paused ? 0f : 1f);
        }

        public HudViewModel CreateHudViewModel()
        {
            return new HudViewModel(Score.Value);
        }

        public PauseMenuViewModel CreatePauseMenuViewModel()
        {
            return new PauseMenuViewModel(Paused);
        }

        public InputViewModel CreateInputViewModel()
        {
            return new InputViewModel(!Paused);
        }
    }
}