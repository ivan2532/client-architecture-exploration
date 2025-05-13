using Features.Game.Views.ViewModels;
using UnityEngine;

namespace Features.Game.Views
{
    public class MainCharacterView : GameView<MainCharacterViewModel>
    {
        [SerializeField] private new Rigidbody rigidbody;

        private void FixedUpdate()
        {
            UpdateVelocity();
        }

        protected override MainCharacterViewModel Initialize()
        {
            return new MainCharacterViewModel(Vector3.zero);
        }

        private void UpdateVelocity()
        {
            rigidbody.linearVelocity = ViewModel.Velocity;
        }
    }
}