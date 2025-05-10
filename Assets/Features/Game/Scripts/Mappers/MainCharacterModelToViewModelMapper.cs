using Features.Game.Model;
using Features.Game.ViewModel;

namespace Features.Game.Mappers
{
    public static class MainCharacterModelToViewModelMapper
    {
        public static MainCharacterViewModel Map(MainCharacterModel model)
        {
            return new MainCharacterViewModel(model.Velocity);
        }
    }
}