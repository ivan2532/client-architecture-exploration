using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.Game.Events;
using Features.Game.ViewModels;
using UnityEngine;

namespace Features.Game.Views
{
    public class PauseMenuView : View<PauseMenuViewModel>
    {
        [SerializeField] private Canvas pauseMenuCanvas;

        protected override PauseMenuViewModel Initialize()
        {
            ViewModelUpdated += OnViewModelUpdated;
            return new PauseMenuViewModel(false);
        }

        public void OnResumeButtonClicked()
        {
            EventBus.Raise(new ResumeButtonClickedEvent());
        }

        public void OnMainMenuButtonClicked()
        {
            EventBus.Raise(new MainMenuButtonClickedEvent());
        }

        private void OnViewModelUpdated(PauseMenuViewModel viewModel)
        {
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            pauseMenuCanvas.enabled = ViewModel.Visible;
        }
    }
}