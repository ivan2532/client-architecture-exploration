using Core.ViewModel;

namespace Features.Game.ViewModel
{
    public record DroneViewModel(float Pitch, float Yaw) : IViewModel;
}