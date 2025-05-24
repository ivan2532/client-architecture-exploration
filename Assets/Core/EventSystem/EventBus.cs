using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.EventSystem
{
    public static class EventBus
    {
        public delegate void EventHandlerDelegate(IEvent @event);

        public delegate void EventHandlerDelegate<in TEvent>(TEvent @event) where TEvent : IEvent;


        private static readonly Dictionary<Type, List<EventHandlerDelegate>> Subscribers = new();

        public static void Subscribe<TEvent>(EventHandlerDelegate<TEvent> eventHandler) where TEvent : IEvent
        {
            Subscribe(typeof(TEvent), @event => eventHandler((TEvent)@event));
        }

        public static void Unsubscribe<TEvent>(EventHandlerDelegate<TEvent> eventHandler) where TEvent : IEvent
        {
            Unsubscribe(typeof(TEvent), @event => eventHandler((TEvent)@event));
        }

        public static void Raise<TEvent>(TEvent @event) where TEvent : IEvent
        {
            Raise(typeof(TEvent), @event);
        }

        public static void Subscribe(Type eventType, EventHandlerDelegate eventHandler)
        {
            if (!Subscribers.ContainsKey(eventType))
            {
                Subscribers[eventType] = new List<EventHandlerDelegate>();
            }

            Subscribers[eventType].Add(eventHandler);
        }

        public static void Unsubscribe(Type eventType, EventHandlerDelegate eventHandler)
        {
            if (Subscribers.ContainsKey(eventType))
            {
                Subscribers[eventType].Remove(eventHandler);
                Subscribers.Remove(eventType);
            }
        }

        public static void Raise(Type eventType, IEvent @event)
        {
            if (Subscribers.TryGetValue(eventType, out var callbacks))
            {
                var callbacksSnapshot = callbacks.ToList();
                callbacksSnapshot.ForEach(callback => callback.Invoke(@event));
            }
        }
    }
}