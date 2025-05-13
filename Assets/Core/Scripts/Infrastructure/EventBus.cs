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

        /// <summary>
        /// This method uses DynamicInvoke and is considered expensive.
        /// Avoid calling in performance-critical code paths.
        /// </summary>
        public static void Raise<TEvent>(Type eventType, TEvent @event) where TEvent : IEvent
        {
            if (!eventType.IsInstanceOfType(@event))
            {
                throw new ArgumentException($"Event {nameof(@event)} is not of type {eventType.FullName}");
            }

            if (Subscribers.TryGetValue(eventType, out var callbacks))
            {
                var callbacksSnapshot = new List<Delegate>(callbacks);
                foreach (var callback in callbacksSnapshot)
                {
                    callback.DynamicInvoke(@event);
                }
            }
        }
    }
}