using Core.Infrastructure;
using Features.Game.Events;
using Features.Game.View.Model;
using UnityEngine;

namespace Features.Game.View.Views
{
    public class DroneView : GameView<DroneViewModel>
    {
        [SerializeField] private Transform droneCamera;
        [SerializeField] private Transform mainCharacter;
        [SerializeField] private Collider dummyTarget;

        public Vector3 OffsetFromMainCharacter => transform.position - mainCharacter.position;
        public Vector3 Position => transform.position;
        public float Pitch => transform.rotation.eulerAngles.y;
        public float Yaw => transform.rotation.eulerAngles.x;

        private void LateUpdate()
        {
            EventBus.Raise(new DroneUpdateEvent(transform.position, mainCharacter.position));
        }

        protected override DroneViewModel Initialize()
        {
            ViewModelUpdated += OnViewModelUpdated;
            return new DroneViewModel(Position, Pitch, Yaw);
        }

        public RaycastShootResult ShootRaycast()
        {
            Physics.Raycast(droneCamera.position, droneCamera.forward, out var hit);
            return new RaycastShootResult(hit.collider == dummyTarget);
        }

        private void OnViewModelUpdated(DroneViewModel viewModel)
        {
            UpdatePosition();
            UpdateCameraOrientation();
        }

        private void UpdatePosition()
        {
            transform.position = ViewModel.Position;
        }

        private void UpdateCameraOrientation()
        {
            droneCamera.rotation = Quaternion.Euler(ViewModel.Yaw, ViewModel.Pitch, 0f);
        }
    }
}