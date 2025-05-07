using System;
using System.Collections.Generic;
using System.Linq;
using Core.Controller;
using Core.Events;
using Core.View;
using JetBrains.Annotations;

namespace Core.Infrastructure
{
    public class ControllerService : IDisposable
    {
        private readonly Dictionary<ViewBase, ControllerBase> _controllers = new();

        public ControllerService()
        {
            EventBus.Subscribe<ViewEnabledEvent>(OnViewEnabled);
            EventBus.Subscribe<ViewDisabledEvent>(OnViewDisabled);
        }

        public void Dispose()
        {
            DisposeAllControllers();

            EventBus.Unsubscribe<ViewEnabledEvent>(OnViewEnabled);
            EventBus.Unsubscribe<ViewDisabledEvent>(OnViewDisabled);
        }

        private void OnViewEnabled(ViewEnabledEvent viewEnabledEvent)
        {
            var controller = TryCreateControllerForView(viewEnabledEvent.View);
            if (controller != null) _controllers.Add(viewEnabledEvent.View, controller);
        }

        private void OnViewDisabled(ViewDisabledEvent viewDisabledEvent)
        {
            var hasController = _controllers.TryGetValue(viewDisabledEvent.View, out var controller);
            if (!hasController) return;

            DisposeController(controller);
            _controllers.Remove(viewDisabledEvent.View);
        }

        private void DisposeAllControllers()
        {
            foreach (var controller in _controllers.Values)
            {
                DisposeController(controller);
            }

            _controllers.Clear();
        }

        private static void DisposeController(ControllerBase controller)
        {
            if (controller is IDisposable disposableController)
            {
                disposableController.Dispose();
            }
        }

        [CanBeNull]
        private static ControllerBase TryCreateControllerForView(ViewBase view)
        {
            var controllerType = GetControllerType(view);
            return controllerType != null
                ? (ControllerBase)Activator.CreateInstance(controllerType, args: view)
                : null;
        }

        [CanBeNull]
        private static Type GetControllerType(ViewBase view)
        {
            var controllerType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.BaseType != null && type.IsSubclassOf(typeof(ControllerBase)))
                .Where(type => type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(ControllerBase<>))
                .FirstOrDefault(type => type.BaseType.GetGenericArguments()[0] == view.GetType());

            return controllerType;
        }
    }
}