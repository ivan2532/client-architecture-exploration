using Features.Game.Events;
using Features.Game.Models;

namespace Features.Game.Domain
{
    public class Game
    {
        public Score Score { get; private set; } = new();
        public bool Paused { get; private set; }
    }
}