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

            _inputActions.Drone.Look.performed += OnLookPerformed;
            _inputActions.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _inputActions.Drone.Look.performed -= OnLookPerformed;
            _inputActions.Disable();
        }

        protected override GameViewModel CreateInitialViewModel()
        {
            return new GameViewModel(drone.ViewModel);
        }

        protected override void OnUpdateViewModel(GameViewModel viewModel)
        {
            drone.UpdateViewModel(viewModel.Drone);
        }

        private void OnLookPerformed(InputAction.CallbackContext context)
        {
            var inputData = context.ReadValue<Vector2>();
            var inputDelta = new LookInputDelta(inputData.x, inputData.y);
            EventBus.Raise(new LookPerformedEvent(inputDelta));
        }
    }
}