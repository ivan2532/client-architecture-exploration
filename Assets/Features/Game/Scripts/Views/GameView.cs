using Core.Infrastructure.ViewController;
using Features.Game.ViewModels;
using UnityEngine;

namespace Features.Game.Views
{
    public class GameView : View<GameViewModel>
    {
        [field: SerializeField] public HudView Hud { get; private set; }
        [field: SerializeField] public PauseMenuView PauseMenu { get; private set; }
        [field: SerializeField] public InputView Input { get; private set; }
        [field: SerializeField] public DroneView Drone { get; private set; }

        private float _pitch;
        private float _yaw;

        protected override GameViewModel Initialize()
        {
            ViewModelUpdated += OnViewModelUpdated;
            return new GameViewModel(false, true, 1f);
        }

        private void OnViewModelUpdated(GameViewModel viewModel)
        {
            UpdateCursorVisibility();
            UpdateTimeScale();
        }

        private void UpdateCursorVisibility()
        {
            Cursor.lockState = ViewModel.ShowCursor ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = ViewModel.ShowCursor;
        }

        private void UpdateTimeScale()
        {
            Time.timeScale = ViewModel.TimeScale;
        }
    }
}