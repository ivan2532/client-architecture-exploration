using Core.ViewModel;

namespace Features.Game.ViewModel
{
    public record GameViewModel(DroneViewModel Drone) : IViewModel;
}