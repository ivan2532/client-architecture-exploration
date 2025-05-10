using UnityEngine;

namespace Features.Game.Configuration
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "ScriptableObjects/GameConfiguration")]
    public class GameConfiguration : ScriptableObject
    {
        [field: SerializeField] public DroneConfiguration Drone { get; private set; }
        [field: SerializeField] public MainCharacterConfiguration MainCharacter { get; private set; }
    }
}