using Features.Game.Configuration;
using Features.Game.Events;

namespace Features.Game.Domain
{
    public class MainCharacter
    {
        public Velocity Velocity => _velocity;

        private readonly MainCharacterConfiguration _configuration;

        private Velocity _velocity;

        public MainCharacter(MainCharacterConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            _velocity.X = movePerformedEvent.NormalizedInput.X * _configuration.MovementSpeed;
            _velocity.Z = movePerformedEvent.NormalizedInput.Y * _configuration.MovementSpeed;
        }

        public void OnMoveCancelled(MoveCancelledEvent moveCancelledEvent)
        {
            _velocity.X = 0f;
            _velocity.Z = 0f;
        }
    }
}