using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.MainMenu.Scripts.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Features.MainMenu.Scripts.Views
{
    public class MainMenuView : View
    {
        [SerializeField] public Button playButton;
        [SerializeField] public Button exitButton;

        private void Awake()
        {
            InitializeButtonListeners();
        }

        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        private void InitializeButtonListeners()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            EventBus.Raise(new PlayButtonClickedEvent());
        }

        private void OnExitButtonClicked()
        {
            EventBus.Raise(new ExitButtonClickedEvent());
        }
    }
}