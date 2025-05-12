using System;

namespace Core.Infrastructure
{
    public class ViewNotCreatedException<TView> : Exception
    {
        public ViewNotCreatedException() : base($"View of type {typeof(TView).FullName} not initialized")
        {
        }
    }
}