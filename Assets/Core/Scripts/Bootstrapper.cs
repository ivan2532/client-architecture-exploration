using UnityEngine;

namespace Core
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var applicationPrefab = Resources.Load<GameObject>("Application");
            var applicationInstance = Object.Instantiate(applicationPrefab);
            Object.DontDestroyOnLoad(applicationInstance);
        }
    }
}
