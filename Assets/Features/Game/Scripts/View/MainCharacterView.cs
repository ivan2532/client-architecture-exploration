using Core.View;
using Features.Game.ViewModel;
using UnityEngine;

namespace Features.Game.View
{
    public class MainCharacterView : ViewBase<MainCharacterViewModel>
    {
        [SerializeField] private new Rigidbody rigidbody;

        private void FixedUpdate()
        {
            UpdateVelocity();
        }

        protected override MainCharacterViewModel CreateInitialViewModel()
        {
            return new MainCharacterViewModel(Vector3.zero);
        }

        private void UpdateVelocity()
        {
            rigidbody.linearVelocity = ViewModel.Velocity;
        }
    }
}