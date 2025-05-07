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

        protected override void Awake()
        {
            base.Awake();
            _inputActions = new GameInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Drone.Look.performed += OnLookPerformed;
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Drone.Look.performed -= OnLookPerformed;
            _inputActions.Disable();
        }

        protected override void InitializeViewModel()
        {
            // TODO: How to ensure drone's view model is initialized?
            ViewModel = new GameViewModel(drone.ViewModel);
        }

        public override void UpdateViewModel(GameViewModel viewModel)
        {
            base.UpdateViewModel(viewModel);
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