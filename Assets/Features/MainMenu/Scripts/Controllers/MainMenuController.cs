using System;
using Core.Infrastructure;
using Core.Infrastructure.ViewController;
using Features.MainMenu.Events;
using Features.MainMenu.Views;
using JetBrains.Annotations;

namespace Features.MainMenu.Controllers
{
    [UsedImplicitly]
    public class MainMenuController : Controller<MainMenuView>, IDisposable
    {
        private readonly MainMenuView _view;

        public MainMenuController(MainMenuView view) : base(view)
        {
            _view = view;
            _view.ShowCursor();
            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<PlayButtonClickedEvent>(OnPlayButtonClicked);
            EventBus.Subscribe<ExitButtonClickedEvent>(OnExitButtonClicked);
        }

        private void UnsubscribeToEvents()
        {
            EventBus.Unsubscribe<PlayButtonClickedEvent>(OnPlayButtonClicked);
            EventBus.Unsubscribe<ExitButtonClickedEvent>(OnExitButtonClicked);
        }

        private void OnPlayButtonClicked(PlayButtonClickedEvent playButtonClickedEvent)
        {
            _view.LoadGame();
        }

        private void OnExitButtonClicked(ExitButtonClickedEvent exitButtonClickedEvent)
        {
            _view.ExitGame();
        }
    }
}