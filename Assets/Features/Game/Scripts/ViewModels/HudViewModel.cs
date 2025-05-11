using Core.Infrastructure;

namespace Features.Game.ViewModels
{
    public record HudViewModel(int Score) : IViewModel;
}