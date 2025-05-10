using Features.Game.Domain;
using Features.Game.ViewModel;

namespace Features.Game.Mappers
{
    public static class DroneToViewModelMapper
    {
        public static DroneViewModel Map(Drone model)
        {
            return new DroneViewModel(model.Position, model.Pitch, model.Yaw);
        }
    }
}