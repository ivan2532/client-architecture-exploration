using Features.Game.Configuration;
using Features.Game.Events;
using UnityEngine;

namespace Features.Game.Domain.Model
{
    public class MainCharacter
    {
        public Vector3 Velocity { get; private set; }

        private readonly MainCharacterConfiguration _configuration;

        public MainCharacter(MainCharacterConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnMovePerformed(MovePerformedEvent movePerformedEvent)
        {
            Velocity = new Vector3(
                movePerformedEvent.NormalizedInput.X * _configuration.MovementSpeed,
                Velocity.y,
                movePerformedEvent.NormalizedInput.Y * _configuration.MovementSpeed
            );
        }

        public void OnMoveCancelled()
        {
            Velocity = new Vector3(0f, Velocity.y, 0f);
        }
    }
}