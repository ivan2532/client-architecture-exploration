using Core.Infrastructure;
using Features.MainMenu.Events;

namespace Features.MainMenu.Views
{
    public abstract class MainMenuView : View
    {
        protected override void RaiseViewCreatedEvent()
        {
            EventBus.Raise(new MainMenuViewCreatedEvent(this));
        }
    }
}