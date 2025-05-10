using Core.ViewModel;
using Features.Game.Model;

namespace Features.Game.ViewModel
{
    public record MainCharacterViewModel(Velocity Velocity) : IViewModel;
}