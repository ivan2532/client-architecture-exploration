using System;
using Core.Infrastructure;
using Features.MainMenu.Events;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Features.MainMenu
{
    public class MainMenuService : IDisposable
    {
        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            SubscribeToEvents();
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
            UnsubscribeFromEvents();
            SceneManager.LoadScene("Game");
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