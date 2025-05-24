using System.Collections;
using Features.Game.Domain;
using Features.MainMenu.Events;
using UnityEditor;
using UnityEngine.SceneManagement;
using Utility;

namespace Features.MainMenu.Domain
{
    public class MainMenuService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        private GameService _gameService;

        public MainMenuService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void ResolveGameService(GameService gameService)
        {
            _gameService = gameService;
        }

        public IEnumerator Load()
        {
            yield return SceneManager.LoadSceneAsync("MainMenu");
        }

        public void OnPlayButtonClicked(PlayButtonClickedEvent playButtonClickedEvent)
        {
            _coroutineRunner.Run(LoadGame());
        }

        public void OnExitButtonClicked(ExitButtonClickedEvent exitButtonClickedEvent)
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