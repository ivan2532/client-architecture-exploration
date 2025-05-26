using System.Collections;
using Features.MainMenu.Ports.Output;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Features.MainMenu.Adapters.Output
{
    public class MainMenuPresenter : IMainMenuPresenter
    {
        public IEnumerator LoadMainMenuScene()
        {
            yield return SceneManager.LoadSceneAsync("MainMenu");
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}