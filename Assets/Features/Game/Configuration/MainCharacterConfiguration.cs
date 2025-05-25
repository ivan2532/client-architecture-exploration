using UnityEngine;

namespace Features.Game.Configuration
{
    [CreateAssetMenu(
        fileName = "MainCharacterConfiguration",
        menuName = "Scriptable Objects/Configuration/Main Character")]
    public class MainCharacterConfiguration : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;
    }
}