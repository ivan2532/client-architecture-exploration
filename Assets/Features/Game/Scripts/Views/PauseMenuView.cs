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

        protected override PauseMenuViewModel CreateInitialViewModel()
        {
            return new PauseMenuViewModel(false);
        }

        protected override void OnViewModelUpdated()
        {
            base.OnViewModelUpdated();
            UpdateVisibility();
        }

        public void OnResumeButtonClicked()
        {
            EventBus.Raise(new ResumeButtonClickedEvent());
        }

        public void OnMainMenuButtonClicked()
        {
            EventBus.Raise(new MainMenuButtonClickedEvent());
        }

        private void UpdateVisibility()
        {
            pauseMenuCanvas.enabled = ViewModel.Visible;
        }
    }
}