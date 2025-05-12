using Core.Infrastructure.ViewController;
using Features.Game.ViewModels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Game.Views
{
    public class GameView : View<GameViewModel>
    {
        [SerializeField] private DroneView drone;
        [SerializeField] private MainCharacterView mainCharacter;
        [SerializeField] private HudView hud;
        [SerializeField] private PauseMenuView pauseMenu;

        public Vector3 DroneOffsetFromMainCharacter => drone.OffsetFromMainCharacter;
        public Vector3 DronePosition => drone.Position;
        public float DronePitch => drone.Pitch;
        public float DroneYaw => drone.Yaw;

        private float _pitch;
        private float _yaw;

        protected override GameViewModel Initialize()
        {
            return new GameViewModel(
                drone.ViewModel,
                mainCharacter.ViewModel,
                hud.ViewModel,
                pauseMenu.ViewModel,
                false,
                true,
                1f
            );
        }

        protected override void OnViewModelUpdated()
        {
            base.OnViewModelUpdated();

            drone.UpdateViewModel(ViewModel.Drone);
            mainCharacter.UpdateViewModel(ViewModel.MainCharacter);
            hud.UpdateViewModel(ViewModel.Hud);
            pauseMenu.UpdateViewModel(ViewModel.PauseMenu);

            UpdateCursorVisibility();
            UpdateTimeScale();
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
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