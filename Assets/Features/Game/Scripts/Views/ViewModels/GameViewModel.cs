using Core.Infrastructure;

namespace Features.Game.Views.ViewModels
{
    public record GameViewModel(
        bool ShowCursor,
        bool InputEnabled,
        float TimeScale
    ) : IViewModel;
}