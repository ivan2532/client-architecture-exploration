using Core.DomainContextSystem;
using Features.Game.Infrastructure;
using Features.MainMenu.Adapters.Input;
using Features.MainMenu.Domain;
using UnityEngine;
using Utility;

namespace Features.MainMenu.Infrastructure
{
    public class MainMenuDomainContext : DomainContext
    {
        [SerializeField] private GameDomainContext gameDomain;

        public MainMenuService Service { get; private set; }

        // TODO IvanB: What about this?
        private readonly ICoroutineRunner _coroutineRunner;

        private MainMenuEventHandler _eventHandler;

        protected override void CreateInputAdapters()
        {
            _eventHandler = new MainMenuEventHandler();
        }

        protected override void CreateOutputAdapters()
        {
        }

        protected override void CreateService()
        {
            Service = new MainMenuService(_coroutineRunner);
        }

        protected override void ResolveInternalCircularDependencies()
        {
            _eventHandler.ResolveService(Service);
        }

        public override void ResolveExternalCircularDependencies()
        {
            Service.ResolveGameService(gameDomain.Service);
        }

        public override void Dispose()
        {
            _eventHandler.Dispose();
        }
    }
}