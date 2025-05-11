using Features.Game.Domain;
using Features.Game.ViewModels;

namespace Features.Game.Mappers
{
    public static class MainCharacterToViewModelMapper
    {
        public static MainCharacterViewModel Map(MainCharacter model)
        {
            return new MainCharacterViewModel(model.Velocity);
        }
    }
}