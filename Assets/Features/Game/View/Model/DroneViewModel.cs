using UnityEngine;

namespace Features.Game.View.Model
{
    public record DroneViewModel(Vector3 Position, float Pitch, float Yaw) : IGameViewModel;
}