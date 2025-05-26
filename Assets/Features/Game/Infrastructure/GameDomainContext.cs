using Core.Bootstrapping;
using Features.Game.Adapters.Input;
using Features.Game.Adapters.Output;
using Features.Game.Configuration;
using Features.Game.Domain;
using Features.MainMenu.Infrastructure;
using UnityEngine;

namespace Features.Game.Infrastructure
{
    [CreateAssetMenu(fileName = "GameDomainContext", menuName = "Scriptable Objects/Domains/Game")]
    public class GameDomainContext : DomainContext
    {
        [SerializeField] private GameConfiguration configuration;
        [SerializeField] private MainMenuDomainContext mainMenuDomain;

        public GameService Service { get; private set; }

        private GameEventHandler _eventHandler;
        private GamePresenter _presenter;
        private GameInputController _inputController;

        protected override void CreateInputAdapters()
        {
            _eventHandler = new GameEventHandler();
        }

        protected override void CreateOutputAdapters()
        {
            _presenter = new GamePresenter();
            _inputController = new GameInputController(_eventHandler);
        }

        protected override void CreateService()
        {
            Service = new GameService(configuration, _presenter, _inputController);
        }

        protected override void ResolveInternalCircularDependencies()
        {
            _eventHandler.ResolveService(Service);
        }

        public override void ResolveExternalCircularDependencies()
        {
            Service.ResolveMainMenuService(mainMenuDomain.Service);
        }

        public override void Dispose()
        {
            _eventHandler.Dispose();
        }
    }
}