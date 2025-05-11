using Core.Infrastructure;

namespace Features.Game.ViewModels
{
    public record GameViewModel(
        bool ShowCursor,
        bool InputEnabled,
        float TimeScale
    ) : IViewModel;
}