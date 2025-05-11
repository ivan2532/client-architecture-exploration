using Core.Infrastructure;
using Features.Game.ViewModels;
using UnityEngine;

namespace Features.Game.Views
{
    public class MainCharacterView : View<MainCharacterView, MainCharacterViewModel>
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