using System.Collections;
using Features.Game.Domain;
using UnityEditor;
using UnityEngine.SceneManagement;
using Utility;

namespace Features.MainMenu.Domain
{
    public class MainMenuService
    {
        private GameService _gameService;

        public void ResolveGameService(GameService gameService)
        {
            _gameService = gameService;
        }

        public IEnumerator Load()
        {
            yield return SceneManager.LoadSceneAsync("MainMenu");
        }

        public void OnPlayButtonClicked()
        {
            CoroutineRunner.Run(LoadGame());
        }

        public void OnExitButtonClicked()
        {
            ExitGame();
        }

        private IEnumerator LoadGame()
        {
            yield return _gameService.Load();
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}