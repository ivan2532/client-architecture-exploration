using Features.Game.Model;
using Features.Game.ViewModel;

namespace Features.Game.Mappers
{
    public static class DroneModelToViewModelMapper
    {
        public static DroneViewModel Map(DroneModel model)
        {
            return new DroneViewModel(model.Pitch, model.Yaw);
        }
    }
}