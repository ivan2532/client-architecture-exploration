using Core.Events;
using Core.Infrastructure;
using Features.Game.Events;
using UnityEngine;

namespace Features.Game.Views
{
    public class GameViewCreatedEventFactory : IViewCreatedEventFactory
    {
        public IViewCreatedEvent Create(MonoBehaviour view)
        {
            return new GameViewCreatedEvent(view);
        }
    }

    public abstract class GameView : View<GameViewCreatedEventFactory>
    {
    }

    public abstract class GameView<TViewModel> : View<GameViewCreatedEventFactory, TViewModel>
        where TViewModel : IViewModel
    {
    }

    public class GameViewProvider : ViewProvider<GameViewCreatedEvent>
    {
    }
}