using Core.EventSystem;
using Features.Game.Events;

namespace Features.Game.View.Views
{
    public abstract class GameView : Core.ViewSystem.View
    {
        protected override void RaiseViewCreatedEvent()
        {
            EventBus.Raise(new GameViewCreatedEvent(this));
        }
    }

    public abstract class GameView<TViewModel> : Core.ViewSystem.View<TViewModel>
    {
        protected override void RaiseViewCreatedEvent()
        {
            EventBus.Raise(new GameViewCreatedEvent(this));
        }
    }
}