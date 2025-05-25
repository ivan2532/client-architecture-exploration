using System;
using System.Collections.Generic;
using Core.DomainContextSystem;
using Features.MainMenu.Infrastructure;
using UnityEngine;
using Utility;

namespace Core.Bootstrapping
{
    public class ApplicationContext : MonoBehaviour, IDisposable
    {
        [SerializeField] private List<DomainContext> domains;

        [SerializeField] private MainMenuDomainContext mainMenuDomain;

        private void OnEnable()
        {
            InitializeDomains();
            ResolveCircularDependencies();
            StartApplication();
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void InitializeDomains()
        {
            domains.ForEach(domain => domain.Initialize());
        }

        private void ResolveCircularDependencies()
        {
            domains.ForEach(domain => domain.ResolveExternalCircularDependencies());
        }

        private void StartApplication()
        {
            CoroutineRunner.Run(mainMenuDomain.Service.Load());
        }

        public void Dispose()
        {
            domains.ForEach(domain => domain.Dispose());
        }
    }
}