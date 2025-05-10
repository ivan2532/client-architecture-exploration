using Core.ViewModel;

namespace Features.Game.ViewModel
{
    public record GameViewModel(
        DroneViewModel Drone,
        MainCharacterViewModel MainCharacter,
        HudViewModel Hud,
        bool ShowCursor
    ) : IViewModel;
}