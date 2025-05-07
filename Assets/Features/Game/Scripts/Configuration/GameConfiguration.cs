using UnityEngine;

namespace Features.Game.Configuration
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "ScriptableObjects/GameConfiguration")]
    public class GameConfiguration : ScriptableObject
    {
        [field: SerializeField] public float LookSensitivity { get; private set; } = 0.1f;

        [field: SerializeField] public float MinimumPitch { get; private set; } = -45f;
        [field: SerializeField] public float MaximumPitch { get; private set; } = 45f;

        [field: SerializeField] public float MinimumYaw { get; private set; } = -45f;
        [field: SerializeField] public float MaximumYaw { get; private set; } = 45f;
    }
}