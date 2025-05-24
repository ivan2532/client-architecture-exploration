using System.Collections;
using Features.Game.Domain;
using Features.MainMenu.Events;
using Features.MainMenu.Ports.Input;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Features.MainMenu.Domain
{
    public class MainMenuService
    {
        private IMainMenuEventHandler _eventHandler;
        private GameService _gameService;
        private CoroutineRunner _coroutineRunner;

        public void Initialize(
            IMainMenuEventHandler eventHandler,
            GameService gameService,
            CoroutineRunner coroutineRunner
        )
        {
            _eventHandler = eventHandler;
            _gameService = gameService;
            _coroutineRunner = coroutineRunner;
        }

        public IEnumerator Load()
        {
            _eventHandler.Enable();
            yield return SceneManager.LoadSceneAsync("MainMenu");
        }

        public void Unload()
        {
            _eventHandler.Disable();
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
            Unload();
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