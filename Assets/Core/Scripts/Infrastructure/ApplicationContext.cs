using System;
using Features.MainMenu;
using UnityEngine;

namespace Core.Infrastructure
{
    public class ApplicationContext : MonoBehaviour, IDisposable
    {
        private MainMenuService _mainMenuService;

        private void OnEnable()
        {
            Initialize();
            StartApplication();
        }

        private void OnDisable()
        {
            Dispose();
        }

        public void Dispose()
        {
            _mainMenuService.Dispose();
        }

        private void Initialize()
        {
            _mainMenuService = new MainMenuService();
        }

        private void StartApplication()
        {
            _mainMenuService.LoadMainMenu();
        }
    }
}