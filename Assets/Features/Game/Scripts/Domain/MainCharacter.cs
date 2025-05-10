using Features.Game.Configuration;
using Features.Game.Events;
using UnityEngine;

namespace Features.Game.Domain
{
    public class MainCharacter
    {
        public Vector3 Velocity => _velocity;

        private readonly MainCharacterConfiguration _configuration;

        private Vector3 _velocity;

        public MainCharacter(MainCharacterConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            _velocity = new Vector3(
                movePerformedEvent.NormalizedInput.X * _configuration.MovementSpeed,
                _velocity.y,
                movePerformedEvent.NormalizedInput.Y * _configuration.MovementSpeed
            );
        }

        public void OnMoveCancelled(MoveCancelledEvent moveCancelledEvent)
        {
            _velocity = new Vector3(0f, _velocity.y, 0f);
        }
    }
}