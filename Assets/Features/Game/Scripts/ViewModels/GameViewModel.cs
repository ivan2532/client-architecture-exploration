using Core.Infrastructure;
using Core.Infrastructure.ViewController;

namespace Features.Game.ViewModels
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