using UnityEngine;

namespace Core.Bootstrapping
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var applicationContextPrefab = Resources.Load<GameObject>("ApplicationContext");
            var applicationContextInstance = Object.Instantiate(applicationContextPrefab);
            Object.DontDestroyOnLoad(applicationContextInstance);
        }
    }
}