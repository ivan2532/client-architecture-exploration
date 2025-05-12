using System.Collections;
using Core.Infrastructure;
using Features.Game.Configuration;
using Features.Game.Domain;
using Features.Game.Events;
using Features.Game.Mappers;
using Features.Game.ViewModels;
using Features.Game.Views;
using Features.MainMenu;
using UnityEngine.SceneManagement;

namespace Features.Game
{
    public class GameService
    {
        private Drone _drone;
        private MainCharacter _mainCharacter;
        private Domain.Game _game;

        private GameConfiguration _configuration;
        private GameViewProvider _viewProvider;
        private MainMenuService _mainMenuService;

        private CoroutineRunner _coroutineRunner;

        public void Initialize(
            GameConfiguration configuration,
            MainMenuService mainMenuService,
            CoroutineRunner coroutineRunner
        )
        {
            _configuration = configuration;
            _viewProvider = new GameViewProvider();
            _mainMenuService = mainMenuService;
            _coroutineRunner = coroutineRunner;
        }

        public void Load()
        {
            _coroutineRunner.StartCoroutine(LoadCoroutine());
        }

        public void Unload()
        {
            UnsubscribeFromEvents();
        }

        private void InitializeDomain()
        {
            _drone = new Drone(
                _configuration.Drone,
                _viewProvider.DroneView.OffsetFromMainCharacter,
                _viewProvider.DroneView.Position,
                _viewProvider.DroneView.Pitch,
                _viewProvider.DroneView.Yaw
            );
            _mainCharacter = new MainCharacter(_configuration.MainCharacter);
            _game = new Domain.Game();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<ShootPerformedEvent>(OnShootPerformed);
            EventBus.Subscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Subscribe<DroneUpdateEvent>(OnDroneUpdate);

            EventBus.Subscribe<MovePerformedEvent>(OnMovePerformed);
            EventBus.Subscribe<MoveCancelledEvent>(OnMoveCancelled);

            EventBus.Subscribe<PausePerformedEvent>(OnPausePerformed);
            EventBus.Subscribe<ResumeButtonClickedEvent>(OnResumeButtonClicked);
            EventBus.Subscribe<MainMenuButtonClickedEvent>(OnMainMenuButtonClicked);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<ShootPerformedEvent>(OnShootPerformed);
            EventBus.Unsubscribe<LookPerformedEvent>(OnLookPerformed);
            EventBus.Unsubscribe<DroneUpdateEvent>(OnDroneUpdate);

            EventBus.Unsubscribe<MovePerformedEvent>(OnMovePerformed);
            EventBus.Unsubscribe<MoveCancelledEvent>(OnMoveCancelled);

            EventBus.Unsubscribe<PausePerformedEvent>(OnPausePerformed);
            EventBus.Unsubscribe<ResumeButtonClickedEvent>(OnResumeButtonClicked);
            EventBus.Unsubscribe<MainMenuButtonClickedEvent>(OnMainMenuButtonClicked);
        }

        private void OnShootPerformed(ShootPerformedEvent shootPerformedEvent)
        {
            var raycastShootResult = _viewProvider.DroneView.ShootRaycast();
            var shootResult = _game.OnShootPerformed(raycastShootResult);
            if (shootResult.ScoreChanged) UpdateHudViewModel();
        }

        private void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            _drone.OnLookPerformed(lookPerformedEvent);
            UpdateDroneViewModel();
        }

        private void OnDroneUpdate(DroneUpdateEvent updateEvent)
        {
            _drone.OnUpdate(updateEvent);
            UpdateDroneViewModel();
        }

        private void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            _mainCharacter.OnMovePerformed(movePerformedEvent);
            UpdateMainCharacterViewModel();
        }

        private void OnMoveCancelled(MoveCancelledEvent moveCancelledEvent)
        {
            _mainCharacter.OnMoveCancelled();
            UpdateMainCharacterViewModel();
        }

        private void OnPausePerformed(PausePerformedEvent pausePerformedEvent)
        {
            _game.OnPausePerformed();
            UpdateInputViewModel();
            UpdateGameViewModel();
            UpdatePauseMenuViewModel();
        }

        private void OnResumeButtonClicked(ResumeButtonClickedEvent resumeButtonClickedEvent)
        {
            _game.OnResumeButtonClicked();
            UpdatePauseMenuViewModel();
            UpdateGameViewModel();
            UpdateInputViewModel();
        }

        private void OnMainMenuButtonClicked(MainMenuButtonClickedEvent mainMenuButtonClickedEvent)
        {
            _game.OnMainMenuButtonClicked();
            UpdateGameViewModel();

            Unload();
            _mainMenuService.Load();
        }

        private IEnumerator LoadCoroutine()
        {
            SubscribeToEvents();
            yield return SceneManager.LoadSceneAsync("Game");
            InitializeDomain();
        }

        private void UpdateGameViewModel()
        {
            _viewProvider.GameView.UpdateViewModel(GameToViewModelMapper.Map(_game));
        }

        private void UpdateDroneViewModel()
        {
            _viewProvider.DroneView.UpdateViewModel(DroneToViewModelMapper.Map(_drone));
        }

        private void UpdateMainCharacterViewModel()
        {
            _viewProvider.MainCharacterView.UpdateViewModel(MainCharacterToViewModelMapper.Map(_mainCharacter));
        }

        private void UpdateHudViewModel()
        {
            _viewProvider.HudView.UpdateViewModel(new HudViewModel(_game.Score.Value));
        }

        private void UpdatePauseMenuViewModel()
        {
            _viewProvider.PauseMenuView.UpdateViewModel(new PauseMenuViewModel(_game.Paused));
        }

        private void UpdateInputViewModel()
        {
            _viewProvider.InputView.UpdateViewModel(new InputViewModel(!_game.Paused));
        }
    }
}