using Features.Game.ViewModels;

namespace Features.Game.Mappers
{
    public static class GameToViewModelMapper
    {
        public static GameViewModel Map(Domain.Game model)
        {
            return new GameViewModel(
                DroneToViewModelMapper.Map(model.Drone),
                MainCharacterToViewModelMapper.Map(model.MainCharacter),
                new HudViewModel(model.Score.Value),
                new PauseMenuViewModel(model.Paused),
                model.Paused,
                !model.Paused,
                model.Paused ? 0f : 1f
            );
        }
    }
}