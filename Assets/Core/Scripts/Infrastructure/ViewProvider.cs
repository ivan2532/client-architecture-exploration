using System;
using System.Collections.Generic;
using Core.Events;
using UnityEngine;

namespace Core.Infrastructure
{
    public abstract class ViewProvider<TView, TViewCreatedEvent> : IDisposable
        where TView : MonoBehaviour
        where TViewCreatedEvent : IViewCreatedEvent
    {
        private readonly Dictionary<Type, TView> _views = new();

        protected ViewProvider()
        {
            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        public TRequestedView GetView<TRequestedView>() where TRequestedView : TView
        {
            if (!_views.ContainsKey(typeof(TRequestedView)) || !_views[typeof(TRequestedView)])
            {
                throw new ViewNotCreatedException<TRequestedView>();
            }

            return (TRequestedView)_views[typeof(TRequestedView)];
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
            _views[view.GetType()] = (TView)view;
        }
    }
}