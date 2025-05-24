using Core.Infrastructure;
using Features.Game.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Game.View.Views
{
    public class PauseMenuView : GameView
    {
        [SerializeField] private Canvas pauseMenuCanvas;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;

        protected override void Awake()
        {
            base.Awake();
            resumeButton.onClick.AddListener(OnResumeButtonClicked);
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        public void Show()
        {
            pauseMenuCanvas.enabled = true;
        }

        public void Hide()
        {
            pauseMenuCanvas.enabled = false;
        }

        private void OnResumeButtonClicked()
        {
            EventBus.Raise(new ResumeButtonClickedEvent());
        }

        private void OnMainMenuButtonClicked()
        {
            EventBus.Raise(new MainMenuButtonClickedEvent());
        }
    }
}