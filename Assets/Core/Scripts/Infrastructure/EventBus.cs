using System;
using System.Collections.Generic;

namespace Core.Infrastructure
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> Subscribers = new();

        public static void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            var type = typeof(TEvent);

            if (!Subscribers.ContainsKey(type))
            {
                Subscribers[type] = new List<Delegate>();
            }

            Subscribers[type].Add(handler);
        }

        public static void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            var type = typeof(TEvent);

            if (Subscribers.ContainsKey(type))
            {
                Subscribers[type].Remove(handler);
                Subscribers.Remove(type);
            }
        }

        public static void Raise<TEvent>(TEvent raisedEvent) where TEvent : IEvent
        {
            var type = raisedEvent.GetType();

            if (Subscribers.TryGetValue(type, out var callbacks))
            {
                foreach (var callback in callbacks)
                {
                    ((Action<TEvent>)callback)?.Invoke(raisedEvent);
                }
            }
        }
    }
}