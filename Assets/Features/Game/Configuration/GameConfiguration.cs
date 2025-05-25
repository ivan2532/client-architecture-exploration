using UnityEngine;

namespace Features.Game.Configuration
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "Scriptable Objects/Configuration/Game")]
    public class GameConfiguration : ScriptableObject
    {
        [field: SerializeField] public DroneConfiguration Drone { get; private set; }
        [field: SerializeField] public MainCharacterConfiguration MainCharacter { get; private set; }
    }
}