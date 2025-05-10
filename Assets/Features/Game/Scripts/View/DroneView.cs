using System;
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

        public float Pitch => transform.rotation.eulerAngles.y;
        public float Yaw => transform.rotation.eulerAngles.x;

        private void LateUpdate()
        {
            EventBus.Raise(new DroneUpdateEvent(transform.position, mainCharacter.position));
        }

        protected override DroneViewModel CreateInitialViewModel()
        {
            return new DroneViewModel(Pitch, Yaw);
        }

        protected override void OnViewModelUpdated()
        {
            base.OnViewModelUpdated();
            UpdateCameraOrientation();
        }

        private void UpdateCameraOrientation()
        {
            droneCamera.rotation = Quaternion.Euler(ViewModel.Yaw, ViewModel.Pitch, 0f);
        }
    }
}