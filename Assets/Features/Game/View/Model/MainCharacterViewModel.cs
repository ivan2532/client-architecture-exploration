using UnityEngine;

namespace Features.Game.View.Model
{
    public record MainCharacterViewModel(Vector3 Velocity) : IGameViewModel;
}