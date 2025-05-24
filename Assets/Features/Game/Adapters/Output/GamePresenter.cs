using Features.Game.Domain.Model;
using Features.Game.Ports.Output;
using Features.Game.View;
using Features.Game.View.Model;
using Features.Game.View.Views;
using UnityEngine;

namespace Features.Game.Adapters.Output
{
    public class GamePresenter : IGamePresenter
    {
        private readonly GameViewProvider _viewProvider = new();

        private DroneView _droneView;
        private MainCharacterView _mainCharacterView;
        private HudView _hudView;
        private InputView _inputView;
        private PauseMenuView _pauseMenuView;

        public void Initialize()
        {
            _droneView = _viewProvider.GetView<DroneView>();
            _mainCharacterView = _viewProvider.GetView<MainCharacterView>();
            _hudView = _viewProvider.GetView<HudView>();
            _inputView = _viewProvider.GetView<InputView>();
            _pauseMenuView = _viewProvider.GetView<PauseMenuView>();
        }

        public DroneStartingState GetDroneStartingState()
        {
            return new DroneStartingState(
                _droneView.OffsetFromMainCharacter,
                _droneView.Position,
                _droneView.Pitch,
                _droneView.Yaw
            );
        }

        public RaycastShootResult ShootRaycastFromDrone()
        {
            return _droneView.ShootRaycast();
        }

        public void UpdateDrone(Drone drone)
        {
            var viewModel = new DroneViewModel(drone.Position, drone.Pitch, drone.Yaw);
            _droneView.UpdateViewModel(viewModel);
        }

        public void UpdateMainCharacter(MainCharacter mainCharacter)
        {
            var viewModel = new MainCharacterViewModel(mainCharacter.Velocity);
            _mainCharacterView.UpdateViewModel(viewModel);
        }

        public void UpdateScore(Score score)
        {
            var viewModel = new HudViewModel(score.Value);
            _hudView.UpdateViewModel(viewModel);
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            _pauseMenuView.Show();
        }

        public void ResumeGame()
        {
            _pauseMenuView.Hide();
            Time.timeScale = 1f;
        }

        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void ShowPauseMenu()
        {
            _pauseMenuView.Show();
        }

        public void HidePauseMenu()
        {
            _pauseMenuView.Hide();
        }
    }
}