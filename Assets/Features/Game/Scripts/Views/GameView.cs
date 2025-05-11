using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.Game.Domain;
using Features.Game.Events;
using Features.Game.ViewModels;
using UnityEngine;
using UnityEngine.InputSystem;
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

        private GameInputActions _inputActions;
        private float _pitch;
        private float _yaw;

        private void Awake()
        {
            _inputActions = new GameInputActions();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeToInputEvents();
            _inputActions.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromInputEvents();
            _inputActions.Disable();
        }

        protected override GameViewModel CreateInitialViewModel()
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
            SetInputEnabled();
            UpdateTimeScale();
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void SubscribeToInputEvents()
        {
            _inputActions.Drone.Look.performed += OnLookPerformed;
            _inputActions.Drone.Shoot.performed += OnShootPerformed;
            _inputActions.MainCharacter.Move.performed += OnMovePerformed;
            _inputActions.MainCharacter.Move.canceled += OnMoveCanceled;
            _inputActions.Menus.Pause.performed += OnPausePerformed;
        }

        private void UnsubscribeFromInputEvents()
        {
            _inputActions.Drone.Look.performed -= OnLookPerformed;
            _inputActions.Drone.Shoot.performed -= OnShootPerformed;
            _inputActions.MainCharacter.Move.performed -= OnMovePerformed;
            _inputActions.MainCharacter.Move.canceled -= OnMoveCanceled;
            _inputActions.Menus.Pause.performed -= OnPausePerformed;
        }

        private void OnLookPerformed(InputAction.CallbackContext context)
        {
            var inputData = context.ReadValue<Vector2>();
            var inputDelta = new LookInputDelta(inputData.x, inputData.y);
            EventBus.Raise(new LookPerformedEvent(inputDelta));
        }

        private void OnShootPerformed(InputAction.CallbackContext context)
        {
            var shootRaycastResult = drone.ShootRaycast();
            EventBus.Raise(new ShootPerformedEvent(shootRaycastResult));
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            var inputData = context.ReadValue<Vector2>();
            var inputVectorNormalized = new Vector2(inputData.x, inputData.y).normalized;
            var input = new MoveInput(inputVectorNormalized.x, inputVectorNormalized.y);
            EventBus.Raise(new MovePerformedEvent(input));
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            EventBus.Raise(new MoveCancelledEvent());
        }

        private void OnPausePerformed(InputAction.CallbackContext context)
        {
            EventBus.Raise(new PausePerformedEvent());
        }

        private void UpdateCursorVisibility()
        {
            Cursor.lockState = ViewModel.ShowCursor ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = ViewModel.ShowCursor;
        }

        private void SetInputEnabled()
        {
            if (ViewModel.InputEnabled) _inputActions.Enable();
            else _inputActions.Disable();
        }

        private void UpdateTimeScale()
        {
            Time.timeScale = ViewModel.TimeScale;
        }
    }
}