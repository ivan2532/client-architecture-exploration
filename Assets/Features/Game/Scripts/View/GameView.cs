using Core.Infrastructure;
using Core.View;
using Features.Game.Events;
using Features.Game.Model;
using Features.Game.ViewModel;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Game.View
{
    public class GameView : ViewBase<GameViewModel>
    {
        [SerializeField] private DroneView drone;
        [SerializeField] private MainCharacterView mainCharacter;

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
            EnableInput();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            DisableInput();
        }

        private void EnableInput()
        {
            _inputActions.Drone.Look.performed += OnLookPerformed;
            _inputActions.MainCharacter.Move.performed += OnMovePerformed;
            _inputActions.MainCharacter.Move.canceled += OnMoveCanceled;
            _inputActions.Enable();
        }

        private void DisableInput()
        {
            _inputActions.Drone.Look.performed -= OnLookPerformed;
            _inputActions.MainCharacter.Move.performed -= OnMovePerformed;
            _inputActions.MainCharacter.Move.canceled -= OnMoveCanceled;
            _inputActions.Disable();
        }

        protected override GameViewModel CreateInitialViewModel()
        {
            return new GameViewModel(drone.ViewModel, mainCharacter.ViewModel);
        }

        protected override void OnViewModelUpdate(GameViewModel viewModel)
        {
            drone.UpdateViewModel(viewModel.Drone);
            mainCharacter.UpdateViewModel(viewModel.MainCharacter);
        }

        private void OnLookPerformed(InputAction.CallbackContext context)
        {
            var inputData = context.ReadValue<Vector2>();
            var inputDelta = new LookInputDelta(inputData.x, inputData.y);
            EventBus.Raise(new LookPerformedEvent(inputDelta));
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
    }
}