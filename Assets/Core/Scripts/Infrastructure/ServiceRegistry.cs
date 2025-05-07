using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Infrastructure
{
    [Serializable]
    public class ServiceRegistry
    {
        [SerializeField] private List<ScriptableObject> scriptableObjectServices;

        private readonly Dictionary<Type, object> _services = new();

        public void Initialize()
        {
            RegisterScriptableObjectServices();
            RegisterHardCodedServices();
        }

        private void RegisterScriptableObjectServices()
        {
            foreach (var scriptableObjectService in scriptableObjectServices)
            {
                Register(scriptableObjectService.GetType(), scriptableObjectService);
            }
        }

        private void RegisterHardCodedServices()
        {
        }

        public void Register(Type serviceType, object serviceImplementation)
        {
            if (!_services.TryAdd(serviceType, serviceImplementation))
            {
                throw new ArgumentException(
                    $"Service implementation for type {serviceType.FullName} already registered.");
            }
        }

        public void Register<TService, TImplementation>(TImplementation serviceImplementation)
        {
            Register(typeof(TService), serviceImplementation);
        }

        public bool Unregister<TService>()
        {
            return _services.Remove(typeof(TService));
        }

        public TService Get<TService>()
        {
            if (!_services.ContainsKey(typeof(TService)))
            {
                throw new ArgumentException($"Service implementation for type {nameof(TService)} not found!");
            }

            return (TService)_services[typeof(TService)];
        }
    }
}