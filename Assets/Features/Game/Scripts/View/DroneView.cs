using Core.View;
using Features.Game.ViewModel;
using UnityEngine;

namespace Features.Game.View
{
    public class DroneView : ViewBase<DroneViewModel>
    {
        [SerializeField] private Camera droneCamera;

        public float Pitch => transform.rotation.eulerAngles.y;
        public float Yaw => transform.rotation.eulerAngles.x;

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
            droneCamera.transform.rotation = Quaternion.Euler(ViewModel.Yaw, ViewModel.Pitch, 0f);
        }
    }
}