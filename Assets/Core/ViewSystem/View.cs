using UnityEngine;

namespace Core.Infrastructure
{
    public abstract class View : MonoBehaviour
    {
        protected virtual void Awake()
        {
            RaiseViewCreatedEvent();
        }

        protected abstract void RaiseViewCreatedEvent();
    }
}