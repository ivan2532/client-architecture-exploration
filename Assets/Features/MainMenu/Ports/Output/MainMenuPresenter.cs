using System.Collections;

namespace Features.MainMenu.Ports.Output
{
    public interface IMainMenuPresenter
    {
        public IEnumerator LoadMainMenuScene();

        public void ExitGame();
    }
}