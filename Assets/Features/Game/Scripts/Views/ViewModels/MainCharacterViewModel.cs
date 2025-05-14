using UnityEngine;

namespace Features.Game.Views.ViewModels
{
    public record MainCharacterViewModel(Vector3 Velocity) : IGameViewModel;
}