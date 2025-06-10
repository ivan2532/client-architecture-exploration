using System;
using System.Collections.Generic;
using Core.EventSystem;
using UnityEngine;

namespace Core.ViewSystem
{
    public abstract class ViewProvider<TViewCreatedEvent> : IDisposable
        where TViewCreatedEvent : IViewCreatedEvent
    {
        private readonly Dictionary<Type, View> _views = new();

        protected ViewProvider()
        {
            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        public TRequestedView GetView<TRequestedView>() where TRequestedView : View
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
            _views[view.GetType()] = (View)view;
        }
    }
}