using Core.ViewModel;
using UnityEngine;

namespace Features.Game.ViewModel
{
    public record DroneViewModel(Vector3 Position, float Pitch, float Yaw) : IViewModel;
}