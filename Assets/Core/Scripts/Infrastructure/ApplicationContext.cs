using Features.Game;
using Features.Game.Configuration;
using Features.MainMenu;
using UnityEngine;

namespace Core.Infrastructure
{
    public class ApplicationContext : MonoBehaviour
    {
        [SerializeField] private GameConfiguration gameConfiguration;

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

            _mainMenuService.Initialize(_gameService);
            _gameService.Initialize(gameConfiguration, _mainMenuService);
        }

        private void StartApplication()
        {
            _mainMenuService.Load();
        }

        private void Unload()
        {
            _mainMenuService.Unload();
            _gameService.Unload();
        }
    }
}