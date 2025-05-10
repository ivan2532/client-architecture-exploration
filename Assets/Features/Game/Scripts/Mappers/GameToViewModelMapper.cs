using Features.Game.ViewModel;

namespace Features.Game.Mappers
{
    public static class GameToViewModelMapper
    {
        public static GameViewModel Map(Domain.Game model)
        {
            return new GameViewModel(
                DroneToViewModelMapper.Map(model.Drone),
                MainCharacterToViewModelMapper.Map(model.MainCharacter)
            );
        }
    }
}