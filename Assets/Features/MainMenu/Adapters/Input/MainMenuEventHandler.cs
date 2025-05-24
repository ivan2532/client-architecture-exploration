using Core.EventSystem;
using Features.MainMenu.Domain;
using Features.MainMenu.Events;
using Features.MainMenu.Ports.Input;

namespace Features.MainMenu.Adapters.Input
{
    public class MainMenuEventHandler : IMainMenuEventHandler
    {
        private MainMenuService _service;

        public void ResolveService(MainMenuService mainMenuService)
        {
            _service = mainMenuService;
        }

        public void Enable()
        {
            SubscribeToEvents();
        }

        public void Disable()
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