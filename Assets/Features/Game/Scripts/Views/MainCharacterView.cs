using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.Game.ViewModels;
using UnityEngine;

namespace Features.Game.Views
{
    public class MainCharacterView : View<MainCharacterViewModel>
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