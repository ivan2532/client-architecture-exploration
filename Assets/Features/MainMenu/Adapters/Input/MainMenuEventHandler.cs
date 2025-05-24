using Core.EventSystem;
using Features.MainMenu.Domain;
using Features.MainMenu.Events;
using Features.MainMenu.Ports.Input;

namespace Features.MainMenu.Adapters.Input
{
    public class MainMenuEventHandler : IMainMenuEventHandler
    {
        private readonly MainMenuService _mainMenuService;

        public MainMenuEventHandler(MainMenuService mainMenuService)
        {
            _mainMenuService = mainMenuService;
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
            _mainMenuService.OnPlayButtonClicked(@event);
        }

        private void OnExitButtonClicked(ExitButtonClickedEvent @event)
        {
            _mainMenuService.OnExitButtonClicked(@event);
        }
    }
}