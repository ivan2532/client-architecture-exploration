using UnityEngine;

namespace Core.Utility
{
    public static class GameObjectExtensions
    {
        public static bool IsMarkedAsDontDestroyOnLoad(this GameObject gameObject)
        {
            return gameObject.scene.name == "DontDestroyOnLoad";
        }
    }
}