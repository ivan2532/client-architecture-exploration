using UnityEngine;

namespace Features.Game.Domain.Model
{
    public record DroneStartingState(Vector3 OffsetFromMainCharacter, Vector3 Position, float Pitch, float Yaw);
}