using System;
using Core.Events;
using Core.Infrastructure;

namespace Features.MainMenu.Views
{
    // TODO: DELETE THIS
    public class MainMenuViewProvider : IDisposable
    {
        public MainMenuView MainMenuView
        {
            get => _mainMenuView ?? throw new ViewNotInitializedException<MainMenuView>();
            private set => _mainMenuView = value;
        }

        private MainMenuView _mainMenuView;

        public MainMenuViewProvider()
        {
            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<ViewEnabledEvent<MainMenuView>>(OnMainMenuViewEnabled);
            EventBus.Subscribe<ViewDisabledEvent<MainMenuView>>(OnMainMenuViewDisabled);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<ViewEnabledEvent<MainMenuView>>(OnMainMenuViewEnabled);
            EventBus.Unsubscribe<ViewDisabledEvent<MainMenuView>>(OnMainMenuViewDisabled);
        }

        private void OnMainMenuViewEnabled(ViewEnabledEvent<MainMenuView> mainMenuViewEnabledEvent)
        {
            MainMenuView = mainMenuViewEnabledEvent.View;
        }

        private void OnMainMenuViewDisabled(ViewDisabledEvent<MainMenuView> obj)
        {
            MainMenuView = null;
        }
    }
}