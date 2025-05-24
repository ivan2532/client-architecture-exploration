using Core.Infrastructure;
using Core.ViewSystem;
using Features.Game.Events;
using Features.Game.View.Views;

namespace Features.Game.View
{
    public class GameViewProvider : ViewProvider<GameView, GameViewCreatedEvent>
    {
    }
}