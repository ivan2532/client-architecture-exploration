using Core.ViewModel;
using UnityEngine;

namespace Features.Game.ViewModel
{
    public record MainCharacterViewModel(Vector3 Velocity) : IViewModel;
}