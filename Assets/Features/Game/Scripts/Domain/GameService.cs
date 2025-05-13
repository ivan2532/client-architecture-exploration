using System.Collections;
using Core.Infrastructure;
using Features.Game.Configuration;
using Features.Game.Domain.Model;
using Features.Game.Events;
using Features.Game.Views;
using Features.MainMenu;
using UnityEngine.SceneManagement;

namespace Features.Game.Domain
{
    public class GameService
    {
        private GameModel _model;
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

        public IEnumerator Load()
        {
            SubscribeToEvents();
            yield return SceneManager.LoadSceneAsync("Game");
            InitializeModel();
        }

        public void Unload()
        {
            UnsubscribeFromEvents();
        }

        private void InitializeModel()
        {
            var droneView = _viewProvider.GetView<DroneView>();
            _model = new GameModel(
                _configuration,
                droneView.OffsetFromMainCharacter,
                droneView.Position,
                droneView.Pitch,
                droneView.Yaw
            );
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
            var raycastShootResult = _viewProvider.GetView<DroneView>().ShootRaycast();
            var shootResult = _model.OnShootPerformed(raycastShootResult);
            if (shootResult.ScoreChanged) UpdateHudViewModel();
        }

        private void OnLookPerformed(LookPerformedEvent lookPerformedEvent)
        {
            _model.OnLookPerformed(lookPerformedEvent);
            UpdateDroneViewModel();
        }

        private void OnDroneUpdate(DroneUpdateEvent updateEvent)
        {
            _model.OnDroneUpdate(updateEvent);
            UpdateDroneViewModel();
        }

        private void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            _model.OnMovePerformed(movePerformedEvent);
            UpdateMainCharacterViewModel();
        }

        private void OnMoveCancelled(MoveCancelledEvent moveCancelledEvent)
        {
            _model.OnMoveCancelled();
            UpdateMainCharacterViewModel();
        }

        private void OnPausePerformed(PausePerformedEvent pausePerformedEvent)
        {
            _model.OnPausePerformed();
            UpdateInputViewModel();
            UpdateGameViewModel();
            UpdatePauseMenuViewModel();
        }

        private void OnResumeButtonClicked(ResumeButtonClickedEvent resumeButtonClickedEvent)
        {
            _model.OnResumeButtonClicked();
            UpdatePauseMenuViewModel();
            UpdateGameViewModel();
            UpdateInputViewModel();
        }

        private void OnMainMenuButtonClicked(MainMenuButtonClickedEvent mainMenuButtonClickedEvent)
        {
            _model.OnMainMenuButtonClicked();
            UpdateGameViewModel();

            Unload();
            _coroutineRunner.Run(_mainMenuService.Load());
        }

        private void UpdateGameViewModel()
        {
            // _viewProvider.GetView<GameView>().UpdateViewModel(_game.CreateViewModel());
        }

        private void UpdateDroneViewModel()
        {
            _viewProvider.GetView<DroneView>().UpdateViewModel(_drone.CreateViewModel());
        }

        private void UpdateMainCharacterViewModel()
        {
            _viewProvider.GetView<MainCharacterView>().UpdateViewModel(_mainCharacter.CreateViewModel());
        }

        private void UpdateHudViewModel()
        {
            _viewProvider.GetView<HudView>().UpdateViewModel(_model.CreateHudViewModel());
        }

        private void UpdatePauseMenuViewModel()
        {
            _viewProvider.GetView<PauseMenuView>().UpdateViewModel(_model.CreatePauseMenuViewModel());
        }

        private void UpdateInputViewModel()
        {
            _viewProvider.GetView<InputView>().UpdateViewModel(_model.CreateInputViewModel());
        }
    }
}