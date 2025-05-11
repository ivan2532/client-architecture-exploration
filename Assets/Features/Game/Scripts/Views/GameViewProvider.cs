using System;
using Core.Events;
using Core.Infrastructure;

namespace Features.Game.Views
{
    public class GameViewProvider : IDisposable
    {
        public GameView GameView => _gameView ?? throw new ViewNotInitializedException<GameView>();
        public DroneView DroneView => _droneView ?? throw new ViewNotInitializedException<DroneView>();

        public MainCharacterView MainCharacterView =>
            _mainCharacterView ?? throw new ViewNotInitializedException<MainCharacterView>();

        public HudView HudView => _hudView ?? throw new ViewNotInitializedException<HudView>();

        public PauseMenuView PauseMenuView => _pauseMenuView ?? throw new ViewNotInitializedException<PauseMenuView>();

        private GameView _gameView;
        private DroneView _droneView;
        private MainCharacterView _mainCharacterView;
        private HudView _hudView;
        private PauseMenuView _pauseMenuView;

        public GameViewProvider()
        {
            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<ViewEnabledEvent<GameView>>(OnGameViewEnabled);
            EventBus.Subscribe<ViewDisabledEvent<GameView>>(OnGameViewDisabled);

            EventBus.Subscribe<ViewEnabledEvent<DroneView>>(OnDroneViewEnabled);
            EventBus.Subscribe<ViewDisabledEvent<DroneView>>(OnDroneViewDisabled);

            EventBus.Subscribe<ViewEnabledEvent<MainCharacterView>>(OnMainCharacterViewEnabled);
            EventBus.Subscribe<ViewDisabledEvent<MainCharacterView>>(OnMainCharacterViewDisabled);

            EventBus.Subscribe<ViewEnabledEvent<HudView>>(OnHudViewEnabled);
            EventBus.Subscribe<ViewDisabledEvent<HudView>>(OnHudViewDisabled);

            EventBus.Subscribe<ViewEnabledEvent<PauseMenuView>>(OnPauseMenuViewEnabled);
            EventBus.Subscribe<ViewDisabledEvent<PauseMenuView>>(OnPauseMenuViewDisabled);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<ViewEnabledEvent<GameView>>(OnGameViewEnabled);
            EventBus.Unsubscribe<ViewDisabledEvent<GameView>>(OnGameViewDisabled);

            EventBus.Unsubscribe<ViewEnabledEvent<DroneView>>(OnDroneViewEnabled);
            EventBus.Unsubscribe<ViewDisabledEvent<DroneView>>(OnDroneViewDisabled);

            EventBus.Unsubscribe<ViewEnabledEvent<MainCharacterView>>(OnMainCharacterViewEnabled);
            EventBus.Unsubscribe<ViewDisabledEvent<MainCharacterView>>(OnMainCharacterViewDisabled);

            EventBus.Unsubscribe<ViewEnabledEvent<HudView>>(OnHudViewEnabled);
            EventBus.Unsubscribe<ViewDisabledEvent<HudView>>(OnHudViewDisabled);

            EventBus.Unsubscribe<ViewEnabledEvent<PauseMenuView>>(OnPauseMenuViewEnabled);
            EventBus.Unsubscribe<ViewDisabledEvent<PauseMenuView>>(OnPauseMenuViewDisabled);
        }

        private void OnGameViewEnabled(ViewEnabledEvent<GameView> gameViewEnabledEvent)
        {
            _gameView = gameViewEnabledEvent.View;
        }

        private void OnGameViewDisabled(ViewDisabledEvent<GameView> gameViewDisabledEvent)
        {
            _gameView = null;
        }

        private void OnDroneViewEnabled(ViewEnabledEvent<DroneView> droneViewEnabledEvent)
        {
            _droneView = droneViewEnabledEvent.View;
        }

        private void OnDroneViewDisabled(ViewDisabledEvent<DroneView> droneViewDisabledEvent)
        {
            _droneView = null;
        }

        private void OnMainCharacterViewEnabled(ViewEnabledEvent<MainCharacterView> obj)
        {
            _mainCharacterView = obj.View;
        }

        private void OnMainCharacterViewDisabled(ViewDisabledEvent<MainCharacterView> obj)
        {
            _mainCharacterView = null;
        }

        private void OnHudViewEnabled(ViewEnabledEvent<HudView> hudViewEnabledEvent)
        {
            _hudView = hudViewEnabledEvent.View;
        }

        private void OnHudViewDisabled(ViewDisabledEvent<HudView> hudViewDisabledEvent)
        {
            _hudView = null;
        }

        private void OnPauseMenuViewEnabled(ViewEnabledEvent<PauseMenuView> pauseMenuViewEnabledEvent)
        {
            _pauseMenuView = pauseMenuViewEnabledEvent.View;
        }

        private void OnPauseMenuViewDisabled(ViewDisabledEvent<PauseMenuView> pauseMenuViewDisabledEvent)
        {
            _pauseMenuView = null;
        }
    }
}