using Core.EventSystem;
using Features.Game.Domain.Model;
using Features.Game.Events;
using Features.Game.View.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Game.View.Views
{
    public class InputView : GameView
    {
        private GameInputActions _inputActions;

        protected override void Awake()
        {
            base.Awake();
            _inputActions = new GameInputActions();
        }

        private void OnEnable()
        {
            SubscribeToInputEvents();
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            UnsubscribeFromInputEvents();
            _inputActions.Disable();
        }

        public void EnableInput()
        {
            _inputActions.Enable();
        }

        public void DisableInput()
        {
            _inputActions.Disable();
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
            EventBus.Raise(new ShootPerformedEvent());
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
    }
}