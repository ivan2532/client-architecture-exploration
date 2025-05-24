using Features.Game.Adapters.Input;
using Features.Game.Ports.Output;

namespace Features.Game.Adapters.Output
{
    public class GameInputController : IGameInputController
    {
        private readonly GameEventHandler _gameEventHandler;

        public GameInputController(GameEventHandler gameEventHandler)
        {
            _gameEventHandler = gameEventHandler;
        }

        public void EnableInput()
        {
            _gameEventHandler.EnableGameInput();
        }

        public void DisableInput()
        {
            _gameEventHandler.DisableGameInput();
        }
    }
}