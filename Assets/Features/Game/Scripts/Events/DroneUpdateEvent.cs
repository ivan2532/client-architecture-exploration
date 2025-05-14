using Core.Events;
using UnityEngine;

namespace Features.Game.Events
{
    public record DroneUpdateEvent(Vector3 DronePosition, Vector3 MainCharacterPosition) : IEvent;
}