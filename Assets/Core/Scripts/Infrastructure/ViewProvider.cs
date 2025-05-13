using System;
using System.Collections.Generic;
using Core.Events;
using UnityEngine;

namespace Core.Infrastructure
{
    public abstract class ViewProvider<TViewCreatedEvent> : IDisposable where TViewCreatedEvent : IViewCreatedEvent
    {
        private readonly Dictionary<Type, MonoBehaviour> _views = new();

        protected ViewProvider()
        {
            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        public TView GetView<TView>()
        {
            if (!_views.ContainsKey(typeof(TView)) || !_views[typeof(TView)])
            {
                throw new ViewNotCreatedException<TView>();
            }

            if (_views[typeof(TView)] is not TView view)
            {
                throw new ArgumentException($"There is no view registered for the given type {typeof(TView)}");
            }

            return view;
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<TViewCreatedEvent>(OnViewCreated);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.Unsubscribe<TViewCreatedEvent>(OnViewCreated);
        }

        private void OnViewCreated(TViewCreatedEvent viewCreatedEvent)
        {
            var view = viewCreatedEvent.GetView();
            _views[view.GetType()] = view;
        }
    }
}