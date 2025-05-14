using UnityEngine;

namespace Features.Game.Models
{
    public record DroneStartingState(Vector3 OffsetFromMainCharacter, Vector3 Position, float Pitch, float Yaw);
}