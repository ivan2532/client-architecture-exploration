using UnityEngine;

namespace Features.Game.Configuration
{
    [CreateAssetMenu(
        fileName = "MainCharacterConfiguration",
        menuName = "ScriptableObjects/MainCharacterConfiguration")]
    public class MainCharacterConfiguration : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;
    }
}