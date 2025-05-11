using Core.Infrastructure;
using Core.Infrastructure.ViewController;

namespace Features.Game.ViewModels
{
    public record HudViewModel(int Score) : IViewModel;
}