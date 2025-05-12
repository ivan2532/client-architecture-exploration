using System;
using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.MainMenu.Events;
using Features.MainMenu.Views;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Features.MainMenu.Controllers
{
    [UsedImplicitly]
    public class MainMenuController : Controller<MainMenuView>, IDisposable
    {
        public MainMenuController(MainMenuView view) : base(view)
        {
            view.ShowCursor();
            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<PlayButtonClickedEvent>(OnPlayButtonClicked);
            EventBus.Subscribe<ExitButtonClickedEvent>(OnExitButtonClicked);
        }

        private void UnsubscribeToEvents()
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