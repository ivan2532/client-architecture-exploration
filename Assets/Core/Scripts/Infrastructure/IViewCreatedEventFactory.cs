using Core.Events;
using UnityEngine;

namespace Core.Infrastructure
{
    public interface IViewCreatedEventFactory
    {
        public IViewCreatedEvent Create(MonoBehaviour view);
    }
}