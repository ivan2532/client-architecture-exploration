using Features.Game.Adapters.Input;
using Features.Game.Adapters.Output;
using Features.Game.Configuration;
using Features.Game.Domain;
using Features.MainMenu.Adapters.Input;
using Features.MainMenu.Domain;
using UnityEngine;

namespace Core.Infrastructure
{
    public class ApplicationContext : MonoBehaviour
    {
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private CoroutineRunner coroutineRunner;

        private MainMenuService _mainMenuService;
        private GameService _gameService;

        private void OnEnable()
        {
            Initialize();
            StartApplication();
        }

        private void OnDisable()
        {
            Unload();
        }

        private void Initialize()
        {
            _mainMenuService = new MainMenuService();
            var mainMenuEventHandler = new MainMenuEventHandler(_mainMenuService);

            var gamePresenter = new GamePresenter();
            _gameService = new GameService();
            var gameEventHandler = new GameEventHandler(_gameService);

            _mainMenuService.Initialize(mainMenuEventHandler, _gameService, coroutineRunner);
            _gameService.Initialize(
                gameConfiguration,
                gameEventHandler,
                gamePresenter,
                _mainMenuService,
                coroutineRunner
            );
        }

        private void StartApplication()
        {
            coroutineRunner.Run(_mainMenuService.Load());
        }

        private void Unload()
        {
            _mainMenuService.Unload();
            _gameService.Unload();
        }
    }
}