using Core.Infrastructure.ViewController;
using Features.Game.ViewModels;
using UnityEngine;

namespace Features.Game.Views
{
    public class MainCharacterView : View<MainCharacterViewModel>
    {
        [SerializeField] private new Rigidbody rigidbody;

        protected override MainCharacterViewModel Initialize()
        {
            return new MainCharacterViewModel(Vector3.zero);
        }

        private void FixedUpdate()
        {
            UpdateVelocity();
        }

        private void UpdateVelocity()
        {
            rigidbody.linearVelocity = ViewModel.Velocity;
        }
    }
}