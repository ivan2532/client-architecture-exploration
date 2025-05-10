using Core.Infrastructure;
using Core.View;
using Features.Game.Events;
using Features.Game.ViewModel;
using UnityEngine;

namespace Features.Game.View
{
    public class DroneView : ViewBase<DroneViewModel>
    {
        [SerializeField] private Transform droneCamera;
        [SerializeField] private Transform mainCharacter;

        public Vector3 OffsetFromMainCharacter => transform.position - mainCharacter.position;
        public Vector3 Position => transform.position;
        public float Pitch => transform.rotation.eulerAngles.y;
        public float Yaw => transform.rotation.eulerAngles.x;

        private void LateUpdate()
        {
            EventBus.Raise(new DroneUpdateEvent(transform.position, mainCharacter.position));
        }

        protected override DroneViewModel CreateInitialViewModel()
        {
            return new DroneViewModel(Position, Pitch, Yaw);
        }

        protected override void OnViewModelUpdated()
        {
            base.OnViewModelUpdated();
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