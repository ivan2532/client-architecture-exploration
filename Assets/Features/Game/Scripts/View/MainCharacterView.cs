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

        private void FixedUpdate()
        {
            UpdateVelocity();
        }

        protected override MainCharacterViewModel CreateInitialViewModel()
        {
            return new MainCharacterViewModel(Velocity.Zero);
        }

        private void UpdateVelocity()
        {
            rigidbody.linearVelocity = VelocityToVector3Mapper.Map(ViewModel.Velocity);
        }
    }
}