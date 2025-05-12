using System.Collections;
using Core.Infrastructure;
using Features.Game;
using Features.MainMenu.Events;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Features.MainMenu
{
    public class MainMenuService
    {
        private GameService _gameService;
        private CoroutineRunner _coroutineRunner;

        public void Initialize(GameService gameService, CoroutineRunner coroutineRunner)
        {
            _gameService = gameService;
            _coroutineRunner = coroutineRunner;
        }

        public void Load()
        {
            _coroutineRunner.StartCoroutine(LoadCoroutine());
        }

        public void Unload()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<PlayButtonClickedEvent>(OnPlayButtonClicked);
            EventBus.Subscribe<ExitButtonClickedEvent>(OnExitButtonClicked);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<PlayButtonClickedEvent>(OnPlayButtonClicked);
            EventBus.Unsubscribe<ExitButtonClickedEvent>(OnExitButtonClicked);
        }

        private void OnPlayButtonClicked(PlayButtonClickedEvent playButtonClickedEvent)
        {
            LoadGame();
        }

        private void OnExitButtonClicked(ExitButtonClickedEvent exitButtonClickedEvent)
        {
            ExitGame();
        }

        private IEnumerator LoadCoroutine()
        {
            SubscribeToEvents();
            yield return SceneManager.LoadSceneAsync("MainMenu");
        }

        private void LoadGame()
        {
            Unload();
            _gameService.Load();
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