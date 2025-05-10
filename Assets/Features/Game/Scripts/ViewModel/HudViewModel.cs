using Core.ViewModel;

namespace Features.Game.ViewModel
{
    public record HudViewModel(int Score) : IViewModel;
}