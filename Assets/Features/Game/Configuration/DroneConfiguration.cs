using UnityEngine;

namespace Features.Game.Configuration
{
    [CreateAssetMenu(fileName = "DroneConfiguration", menuName = "ScriptableObjects/DroneConfiguration")]
    public class DroneConfiguration : ScriptableObject
    {
        [field: SerializeField] public float FollowSmoothTime { get; private set; } = 0.1f;

        [field: SerializeField] public float LookSensitivity { get; private set; } = 0.1f;

        [field: SerializeField] public float MinimumPitch { get; private set; } = -45f;
        [field: SerializeField] public float MaximumPitch { get; private set; } = 45f;

        [field: SerializeField] public float MinimumYaw { get; private set; } = -45f;
        [field: SerializeField] public float MaximumYaw { get; private set; } = 45f;
    }
}