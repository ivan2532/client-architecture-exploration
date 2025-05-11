using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using UnityEngine;

namespace Features.Game.ViewModels
{
    public record DroneViewModel(Vector3 Position, float Pitch, float Yaw) : IViewModel;
}