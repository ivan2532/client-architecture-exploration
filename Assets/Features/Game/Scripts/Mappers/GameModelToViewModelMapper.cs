using Features.Game.Model;
using Features.Game.ViewModel;

namespace Features.Game.Mappers
{
    public static class GameModelToViewModelMapper
    {
        public static GameViewModel Map(GameModel model)
        {
            return new GameViewModel(new DroneViewModel(model.Drone.Pitch, model.Drone.Yaw));
        }
    }
}