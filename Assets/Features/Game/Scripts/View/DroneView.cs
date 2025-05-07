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

        protected override void InitializeViewModel()
        {
            ViewModel = new DroneViewModel(Pitch, Yaw);
        }

        public override void UpdateViewModel(DroneViewModel droneViewModel)
        {
            base.UpdateViewModel(droneViewModel);
            UpdateCameraOrientation();
        }

        private void UpdateCameraOrientation()
        {
            droneCamera.transform.rotation = Quaternion.Euler(ViewModel.Pitch, ViewModel.Yaw, 0f);
        }
    }
}