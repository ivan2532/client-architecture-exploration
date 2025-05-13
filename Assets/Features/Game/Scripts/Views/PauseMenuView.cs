using Core.Infrastructure;
using Features.Game.Events;
using Features.Game.Views.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Game.Views
{
    public class PauseMenuView : View<PauseMenuView, PauseMenuViewModel>
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

        protected override PauseMenuViewModel Initialize()
        {
            return new PauseMenuViewModel(false);
        }

        protected override void OnViewModelUpdated()
        {
            base.OnViewModelUpdated();
            UpdateVisibility();
        }

        private void OnResumeButtonClicked()
        {
            EventBus.Raise(new ResumeButtonClickedEvent());
        }

        private void OnMainMenuButtonClicked()
        {
            EventBus.Raise(new MainMenuButtonClickedEvent());
        }

        private void UpdateVisibility()
        {
            pauseMenuCanvas.enabled = ViewModel.Visible;
        }
    }
}