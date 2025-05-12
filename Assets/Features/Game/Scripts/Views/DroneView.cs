using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.Game.Events;
using Features.Game.ViewModels;
using UnityEngine;

namespace Features.Game.Views
{
    public class DroneView : View<DroneViewModel>
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
            return new DroneViewModel(Position, Pitch, Yaw);
        }

        protected override void OnViewModelUpdated()
        {
            base.OnViewModelUpdated();
            UpdatePosition();
            UpdateCameraOrientation();
        }

        public RaycastShootResult ShootRaycast()
        {
            Physics.Raycast(droneCamera.position, droneCamera.forward, out var hit);
            return new RaycastShootResult(hit.collider == dummyTarget);
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