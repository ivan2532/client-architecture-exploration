using UnityEngine;
using Utility;

namespace Core.Bootstrapping
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            CoroutineRunner.Create();
            CreateApplicationContext();
        }

        private static void CreateApplicationContext()
        {
            var applicationContextPrefab = Resources.Load<GameObject>("ApplicationContext");
            var applicationContextInstance = Object.Instantiate(applicationContextPrefab);
            Object.DontDestroyOnLoad(applicationContextInstance);
        }
    }
}