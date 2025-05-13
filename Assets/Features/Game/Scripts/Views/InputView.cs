using Core.Infrastructure;
using Features.Game.Domain;
using Features.Game.Domain.Model;
using Features.Game.Events;
using Features.Game.Views.ViewModels;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Game.Views
{
    public class InputView : View<InputView, InputViewModel>
    {
        private GameInputActions _inputActions;

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

        protected override InputViewModel Initialize()
        {
            _inputActions = new GameInputActions();
            return new InputViewModel(true);
        }

        protected override void OnViewModelUpdated()
        {
            base.OnViewModelUpdated();
            UpdateInputEnabled();
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

        private void UpdateInputEnabled()
        {
            if (ViewModel.InputEnabled) _inputActions.Enable();
            else _inputActions.Disable();
        }
    }
}