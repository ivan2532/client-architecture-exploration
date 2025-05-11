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
        private readonly GameService _gameService;

        public MainMenuService(GameService gameService)
        {
            _gameService = gameService;
        }

        public void Load()
        {
            SceneManager.LoadScene("MainMenu");
            SubscribeToEvents();
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