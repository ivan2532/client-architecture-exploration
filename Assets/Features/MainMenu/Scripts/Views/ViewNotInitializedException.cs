using System;

namespace Features.MainMenu.Views
{
    public class ViewNotInitializedException<TView> : Exception
    {
        public ViewNotInitializedException() : base($"View of type {typeof(TView).FullName} not initialized")
        {
        }
    }
}