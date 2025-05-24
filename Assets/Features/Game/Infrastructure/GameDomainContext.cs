using System;
using Features.Game.Adapters.Input;
using Features.Game.Adapters.Output;
using Features.Game.Configuration;
using Features.Game.Domain;
using Features.MainMenu.Domain;
using Utility;

namespace Features.Game.Infrastructure
{
    public class GameDomainContext : IDisposable
    {
        public GameService Service { get; private set; }

        private readonly GameConfiguration _configuration;
        private readonly ICoroutineRunner _coroutineRunner;

        private GameEventHandler _eventHandler;
        private GamePresenter _presenter;
        private GameInputController _inputController;

        private GameDomainContext(GameConfiguration configuration, ICoroutineRunner coroutineRunner)
        {
            _configuration = configuration;
            _coroutineRunner = coroutineRunner;
        }

        public void Dispose()
        {
            _eventHandler.Dispose();
        }

        public static GameDomainContext Create(GameConfiguration configuration, ICoroutineRunner coroutineRunner)
        {
            var context = new GameDomainContext(configuration, coroutineRunner);
            context.CreateInputAdapters();
            context.CreateOutputAdapters();
            context.CreateService();
            context.ResolveInternalCircularDependencies();
            return context;
        }

        public void ResolveCircularDependencies(MainMenuService mainMenuService)
        {
            Service.ResolveMainMenuService(mainMenuService);
        }

        private void CreateInputAdapters()
        {
            _eventHandler = new GameEventHandler();
        }

        private void CreateOutputAdapters()
        {
            _presenter = new GamePresenter();
            _inputController = new GameInputController(_eventHandler);
        }

        private void CreateService()
        {
            Service = new GameService(_configuration, _presenter, _inputController, _coroutineRunner);
        }

        private void ResolveInternalCircularDependencies()
        {
            _eventHandler.ResolveService(Service);
        }
    }
}