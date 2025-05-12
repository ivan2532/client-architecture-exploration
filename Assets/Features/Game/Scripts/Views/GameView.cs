using Core.Infrastructure;
using Features.Game.ViewModels;
using UnityEngine;

namespace Features.Game.Views
{
    public class GameView : View<GameView, GameViewModel>
    {
        [SerializeField] private DroneView drone;
        [SerializeField] private MainCharacterView mainCharacter;
        [SerializeField] private HudView hud;
        [SerializeField] private PauseMenuView pauseMenu;

        private float _pitch;
        private float _yaw;

        protected override GameViewModel Initialize()
        {
            return new GameViewModel(false, true, 1f);
        }

        protected override void OnViewModelUpdated()
        {
            base.OnViewModelUpdated();
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