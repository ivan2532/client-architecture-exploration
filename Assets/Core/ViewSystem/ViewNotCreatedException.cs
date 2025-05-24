using System;

namespace Core.ViewSystem
{
    public class ViewNotCreatedException<TView> : Exception
    {
        public ViewNotCreatedException() : base($"View of type {typeof(TView).FullName} not initialized")
        {
        }
    }
}