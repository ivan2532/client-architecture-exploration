using Core.Infrastructure;
using Features.MainMenu.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Features.MainMenu.Views
{
    public class MainMenuView : View<MainMenuView>
    {
        [SerializeField] public Button playButton;
        [SerializeField] public Button exitButton;

        private void Awake()
        {
            InitializeButtonListeners();
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