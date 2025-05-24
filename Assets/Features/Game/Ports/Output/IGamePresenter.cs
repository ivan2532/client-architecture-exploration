using Features.Game.Domain.Model;
using Features.Game.View.Model;

namespace Features.Game.Ports.Output
{
    public interface IGamePresenter
    {
        public void Initialize();

        public DroneStartingState GetDroneStartingState();
        public RaycastShootResult ShootRaycastFromDrone();
        public void UpdateDrone(Drone drone);

        public void UpdateMainCharacter(MainCharacter mainCharacter);

        public void UpdateScore(Score modelScore);

        public void PauseGame();
        public void ResumeGame();

        public void ShowCursor();
        public void HideCursor();

        public void ShowPauseMenu();
        public void HidePauseMenu();
    }
}