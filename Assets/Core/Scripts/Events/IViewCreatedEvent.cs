using Core.Infrastructure;
using UnityEngine;

namespace Core.Events
{
    public interface IViewCreatedEvent : IEvent
    {
        public MonoBehaviour GetView();
    }
}