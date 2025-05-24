using Core.Infrastructure;
using Features.MainMenu.Events;

namespace Features.MainMenu.View.Views
{
    public abstract class MainMenuView : Core.Infrastructure.View
    {
        protected override void RaiseViewCreatedEvent()
        {
            EventBus.Raise(new MainMenuViewCreatedEvent(this));
        }
    }
}