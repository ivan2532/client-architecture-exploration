using Features.Game.Model;
using Features.Game.ViewModel;

namespace Features.Game.Mappers
{
    public static class GameModelToViewModelMapper
    {
        public static GameViewModel Map(GameModel model)
        {
            return new GameViewModel(
                DroneModelToViewModelMapper.Map(model.Drone),
                MainCharacterModelToViewModelMapper.Map(model.MainCharacter)
            );
        }
    }
}