using Core.ViewModel;

namespace Features.Game.ViewModel
{
    public record GameViewModel(
        DroneViewModel Drone,
        MainCharacterViewModel MainCharacter,
        HudViewModel Hud,
        PauseMenuViewModel PauseMenu,
        bool ShowCursor,
        bool InputEnabled,
        float TimeScale
    ) : IViewModel;
}