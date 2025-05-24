using System;
using Features.Game.Infrastructure;
using Features.MainMenu.Infrastructure;
using UnityEngine;
using Utility;

namespace Core.Bootstrapping
{
    public class ApplicationContext : MonoBehaviour, IDisposable
    {
        [SerializeField] private ScriptableObjectRepository scriptableObjectRepository;
        [SerializeField] private CoroutineRunner coroutineRunner;

        private MainMenuDomainContext _mainMenuDomain;
        private GameDomainContext _gameDomain;

        private void OnEnable()
        {
            CreateDomains();
            ResolveCircularDependencies();
            StartApplication();
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void CreateDomains()
        {
            _mainMenuDomain = MainMenuDomainContext.Create(coroutineRunner);
            _gameDomain = GameDomainContext.Create(scriptableObjectRepository.GameConfiguration, coroutineRunner);
        }

        private void ResolveCircularDependencies()
        {
            _mainMenuDomain.ResolveCircularDependencies(_gameDomain.Service);
            _gameDomain.ResolveCircularDependencies(_mainMenuDomain.Service);
        }

        private void StartApplication()
        {
            coroutineRunner.Run(_mainMenuDomain.Service.Load());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}