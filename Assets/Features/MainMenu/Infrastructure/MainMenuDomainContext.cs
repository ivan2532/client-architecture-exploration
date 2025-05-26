using Core.Bootstrapping;
using Features.Game.Infrastructure;
using Features.MainMenu.Adapters.Input;
using Features.MainMenu.Adapters.Output;
using Features.MainMenu.Domain;
using UnityEngine;

namespace Features.MainMenu.Infrastructure
{
    [CreateAssetMenu(fileName = "MainMenuDomainContext", menuName = "Scriptable Objects/Domains/Main Menu")]
    public class MainMenuDomainContext : DomainContext
    {
        [SerializeField] private GameDomainContext gameDomain;

        public MainMenuService Service { get; private set; }

        private MainMenuEventHandler _eventHandler;
        private MainMenuPresenter _presenter;

        protected override void CreateInputAdapters()
        {
            _eventHandler = new MainMenuEventHandler();
        }

        protected override void CreateOutputAdapters()
        {
            _presenter = new MainMenuPresenter();
        }

        protected override void CreateService()
        {
            Service = new MainMenuService(_presenter);
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