using System.Collections;
using UnityEngine;

namespace Utility
{
    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _runner;

        public static void Create()
        {
            _runner ??= new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(_runner.gameObject);
        }

        public static void Run(IEnumerator coroutine)
        {
            _runner.StartCoroutine(coroutine);
        }
    }
}