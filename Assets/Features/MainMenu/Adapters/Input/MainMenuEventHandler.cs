using System;
using Core.EventSystem;
using Features.MainMenu.Domain;
using Features.MainMenu.Events;

namespace Features.MainMenu.Adapters.Input
{
    public class MainMenuEventHandler : IDisposable
    {
        private MainMenuService _service;

        public void ResolveService(MainMenuService mainMenuService)
        {
            _service = mainMenuService;
            SubscribeToEvents();
        }

        public void Dispose()
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

        private void OnPlayButtonClicked(PlayButtonClickedEvent @event)
        {
            _service.OnPlayButtonClicked(@event);
        }

        private void OnExitButtonClicked(ExitButtonClickedEvent @event)
        {
            _service.OnExitButtonClicked(@event);
        }
    }
}