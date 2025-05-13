using Core.Events;
using Core.Infrastructure;
using Features.MainMenu.Events;
using UnityEngine;

namespace Features.MainMenu.Views
{
    public class MainMenuViewCreatedEventFactory : IViewCreatedEventFactory
    {
        public IViewCreatedEvent Create(MonoBehaviour view)
        {
            return new MainMenuViewCreatedEvent(view);
        }
    }

    public abstract class MainMenuView : View<MainMenuViewCreatedEventFactory>
    {
    }

    public class MainMenuViewProvider : ViewProvider<MainMenuViewCreatedEvent>
    {
    }
}