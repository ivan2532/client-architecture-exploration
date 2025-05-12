using System.Collections;
using UnityEngine;

namespace Core.Infrastructure
{
    public class CoroutineRunner : MonoBehaviour
    {
        public void Run(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}