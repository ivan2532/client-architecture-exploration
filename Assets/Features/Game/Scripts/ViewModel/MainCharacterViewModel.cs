using Core.ViewModel;
using Features.Game.Domain;

namespace Features.Game.ViewModel
{
    public record MainCharacterViewModel(Velocity Velocity) : IViewModel;
}