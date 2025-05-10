using Core.View;
using Features.Game.Mappers;
using Features.Game.Model;
using Features.Game.ViewModel;
using UnityEngine;

namespace Features.Game.View
{
    public class MainCharacterView : ViewBase<MainCharacterViewModel>
    {
        [SerializeField] private new Rigidbody rigidbody;

        private bool _velocityUpdatePending;

        private void FixedUpdate()
        {
            if (_velocityUpdatePending)
            {
                UpdateVelocity();
                _velocityUpdatePending = false;
            }
        }

        protected override MainCharacterViewModel CreateInitialViewModel()
        {
            return new MainCharacterViewModel(Velocity.Zero);
        }

        protected override void OnUpdateViewModel(MainCharacterViewModel viewModel)
        {
            _velocityUpdatePending = true;
        }

        private void UpdateVelocity()
        {
            rigidbody.linearVelocity = VelocityToVector3Mapper.Map(ViewModel.Velocity);
        }
    }
}