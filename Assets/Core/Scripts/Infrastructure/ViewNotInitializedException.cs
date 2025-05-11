using System;

namespace Core.Infrastructure
{
    public class ViewNotInitializedException<TView> : Exception
    {
        public ViewNotInitializedException() : base($"View of type {typeof(TView).FullName} not initialized")
        {
        }
    }
}