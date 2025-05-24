using UnityEngine;

namespace Core.ViewSystem
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