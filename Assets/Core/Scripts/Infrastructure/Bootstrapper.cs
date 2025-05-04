using UnityEngine;

namespace Core.Infrastructure
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
