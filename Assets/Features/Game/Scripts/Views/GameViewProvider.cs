using System;
using Core.Events;
using Core.Infrastructure;

namespace Features.Game.Views
{
    public class GameViewProvider : IDisposable
    {
        public GameView GameView =>
            _gameView ? _gameView : throw new ViewNotCreatedException<GameView>();

        public DroneView DroneView =>
            _droneView ? _droneView : throw new ViewNotCreatedException<DroneView>();

        public MainCharacterView MainCharacterView =>
            _mainCharacterView ? _mainCharacterView : throw new ViewNotCreatedException<MainCharacterView>();

        public HudView HudView =>
            _hudView ? _hudView : throw new ViewNotCreatedException<HudView>();

        public PauseMenuView PauseMenuView =>
            _pauseMenuView ? _pauseMenuView : throw new ViewNotCreatedException<PauseMenuView>();

        public InputView InputView =>
            _inputView ? _inputView : throw new ViewNotCreatedException<InputView>();

        private GameView _gameView;
        private DroneView _droneView;
        private MainCharacterView _mainCharacterView;
        private HudView _hudView;
        private PauseMenuView _pauseMenuView;
        private InputView _inputView;

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
            EventBus.Subscribe<ViewCreatedEvent<GameView>>(OnGameViewCreated);
            EventBus.Subscribe<ViewCreatedEvent<DroneView>>(OnDroneViewCreated);
            EventBus.Subscribe<ViewCreatedEvent<MainCharacterView>>(OnMainCharacterViewCreated);
            EventBus.Subscribe<ViewCreatedEvent<HudView>>(OnHudViewCreated);
            EventBus.Subscribe<ViewCreatedEvent<PauseMenuView>>(OnPauseMenuViewCreated);
            EventBus.Subscribe<ViewCreatedEvent<InputView>>(OnInputViewCreated);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<ViewCreatedEvent<GameView>>(OnGameViewCreated);
            EventBus.Unsubscribe<ViewCreatedEvent<DroneView>>(OnDroneViewCreated);
            EventBus.Unsubscribe<ViewCreatedEvent<MainCharacterView>>(OnMainCharacterViewCreated);
            EventBus.Unsubscribe<ViewCreatedEvent<HudView>>(OnHudViewCreated);
            EventBus.Unsubscribe<ViewCreatedEvent<PauseMenuView>>(OnPauseMenuViewCreated);
            EventBus.Unsubscribe<ViewCreatedEvent<InputView>>(OnInputViewCreated);
        }

        private void OnGameViewCreated(ViewCreatedEvent<GameView> gameViewCreatedEvent)
        {
            _gameView = gameViewCreatedEvent.View;
        }

        private void OnDroneViewCreated(ViewCreatedEvent<DroneView> droneViewCreatedEvent)
        {
            _droneView = droneViewCreatedEvent.View;
        }

        private void OnMainCharacterViewCreated(ViewCreatedEvent<MainCharacterView> mainCharacterViewCreatedEvent)
        {
            _mainCharacterView = mainCharacterViewCreatedEvent.View;
        }

        private void OnHudViewCreated(ViewCreatedEvent<HudView> hudViewCreatedEvent)
        {
            _hudView = hudViewCreatedEvent.View;
        }

        private void OnPauseMenuViewCreated(ViewCreatedEvent<PauseMenuView> pauseMenuViewCreatedEvent)
        {
            _pauseMenuView = pauseMenuViewCreatedEvent.View;
        }

        private void OnInputViewCreated(ViewCreatedEvent<InputView> inputViewCreatedEvent)
        {
            _inputView = inputViewCreatedEvent.View;
        }
    }
}