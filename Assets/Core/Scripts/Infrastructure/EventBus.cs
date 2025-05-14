using System;
using System.Collections.Generic;

namespace Core.Infrastructure
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> Subscribers = new();

        public static void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            if (!Subscribers.ContainsKey(typeof(TEvent)))
            {
                Subscribers[typeof(TEvent)] = new List<Delegate>();
            }

            Subscribers[typeof(TEvent)].Add(handler);
        }

        public static void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            if (Subscribers.ContainsKey(typeof(TEvent)))
            {
                Subscribers[typeof(TEvent)].Remove(handler);
                Subscribers.Remove(typeof(TEvent));
            }
        }

        public static void Raise<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (Subscribers.TryGetValue(typeof(TEvent), out var callbacks))
            {
                var callbacksSnapshot = new List<Delegate>(callbacks);
                foreach (var callback in callbacksSnapshot)
                {
                    ((Action<TEvent>)callback)?.Invoke(@event);
                }
            }
        }
    }

    public interface IEvent
    {
    }
}