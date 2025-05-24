using System;
using Features.Game.Domain;
using Features.MainMenu.Adapters.Input;
using Features.MainMenu.Domain;
using Utility;

namespace Features.MainMenu.Infrastructure
{
    public class MainMenuDomainContext : IDisposable
    {
        public MainMenuService Service { get; private set; }

        private readonly ICoroutineRunner _coroutineRunner;

        private MainMenuEventHandler _eventHandler;

        private MainMenuDomainContext(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Dispose()
        {
            _eventHandler.Dispose();
        }

        public static MainMenuDomainContext Create(ICoroutineRunner coroutineRunner)
        {
            var context = new MainMenuDomainContext(coroutineRunner);
            context.CreateInputAdapters();
            context.CreateService();
            context.ResolveInternalCircularDependencies();
            return context;
        }

        public void ResolveCircularDependencies(GameService gameService)
        {
            Service.ResolveGameService(gameService);
        }

        private void CreateInputAdapters()
        {
            _eventHandler = new MainMenuEventHandler();
        }

        private void CreateService()
        {
            Service = new MainMenuService(_coroutineRunner);
        }

        private void ResolveInternalCircularDependencies()
        {
            _eventHandler.ResolveService(Service);
        }
    }
}