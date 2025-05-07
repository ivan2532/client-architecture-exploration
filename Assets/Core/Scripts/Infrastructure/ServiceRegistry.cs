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

        public void Register<TService, TImplementation>(TImplementation serviceImplementation)
        {
            Register(typeof(TService), serviceImplementation);
        }

        public TService Get<TService>()
        {
            return (TService)Get(typeof(TService));
        }

        public void Register(Type serviceType, object serviceImplementation)
        {
            if (!_services.TryAdd(serviceType, serviceImplementation))
            {
                throw new ArgumentException(
                    $"Service implementation for type {serviceType.FullName} already registered.");
            }
        }

        public object Get(Type serviceType)
        {
            if (_services.TryGetValue(serviceType, out var service))
            {
                return service;
            }

            throw new ArgumentException($"Service implementation for type {serviceType.FullName} not found!");
        }

        public bool Unregister<TService>()
        {
            return _services.Remove(typeof(TService));
        }
    }
}