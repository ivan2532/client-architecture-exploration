using Features.Game;
using Features.Game.Configuration;
using Features.Game.Domain;
using Features.MainMenu;
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
            _gameService = new GameService();

            _mainMenuService.Initialize(_gameService, coroutineRunner);
            _gameService.Initialize(gameConfiguration, _mainMenuService, coroutineRunner);
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