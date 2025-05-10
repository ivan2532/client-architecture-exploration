using Features.Game.Configuration;
using Features.Game.Events;

namespace Features.Game.Model
{
    public class MainCharacterModel
    {
        private readonly MainCharacterConfiguration _configuration;

        private Velocity _velocity;

        public MainCharacterModel(MainCharacterConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            _velocity.X = movePerformedEvent.NormalizedInput.X;
            _velocity.Z = movePerformedEvent.NormalizedInput.Y;
        }

        public void OnMoveCancelled(MoveCancelledEvent moveCancelledEvent)
        {
            _velocity.X = 0f;
            _velocity.Z = 0f;
        }
    }
}