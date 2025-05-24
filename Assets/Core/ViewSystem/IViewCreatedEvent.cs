using Core.EventSystem;
using UnityEngine;

namespace Core.ViewSystem
{
    public interface IViewCreatedEvent : IEvent
    {
        public MonoBehaviour GetView();
    }
}