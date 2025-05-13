using Core.Infrastructure;

namespace Features.Game.Views.ViewModels
{
    public record HudViewModel(int Score) : IViewModel;
}