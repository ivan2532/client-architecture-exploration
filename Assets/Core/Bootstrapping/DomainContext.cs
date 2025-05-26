using System;
using UnityEngine;

namespace Core.Bootstrapping
{
    public abstract class DomainContext : ScriptableObject, IDisposable
    {
        public void Initialize()
        {
            CreateInputAdapters();
            CreateOutputAdapters();
            CreateService();
            ResolveInternalCircularDependencies();
        }

        public abstract void Dispose();

        public abstract void ResolveExternalCircularDependencies();

        protected abstract void CreateInputAdapters();

        protected abstract void CreateOutputAdapters();

        protected abstract void CreateService();

        protected abstract void ResolveInternalCircularDependencies();
    }
}