using Features.Game.ViewModels;

namespace Features.Game.Mappers
{
    public static class GameToViewModelMapper
    {
        public static GameViewModel Map(Domain.Game model)
        {
            return new GameViewModel(
                model.ShowCursor,
                !model.Paused,
                model.Paused ? 0f : 1f
            );
        }
    }
}