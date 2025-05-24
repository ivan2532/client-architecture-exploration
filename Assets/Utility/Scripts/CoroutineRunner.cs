using System.Collections;
using UnityEngine;

namespace Utility
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public void Run(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }

    public interface ICoroutineRunner
    {
        public void Run(IEnumerator coroutine);
    }
}