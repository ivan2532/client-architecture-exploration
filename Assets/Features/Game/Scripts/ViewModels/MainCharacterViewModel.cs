using Core.Infrastructure;
using UnityEngine;

namespace Features.Game.ViewModels
{
    public record MainCharacterViewModel(Vector3 Velocity) : IViewModel;
}