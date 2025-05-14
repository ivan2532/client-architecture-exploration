using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Events;
using Features.Game.Views;
using JetBrains.Annotations;

namespace Core.Infrastructure.ViewController
{
    public class ControllerService : IDisposable
    {
        private readonly ServiceRegistry _serviceRegistry;

        private readonly Dictionary<View, Controller> _controllers = new();

        public ControllerService(ServiceRegistry serviceRegistry)
        {
            _serviceRegistry = serviceRegistry;

            EventBus.Subscribe<ViewEnabledEvent>(OnViewEnabled);
            EventBus.Subscribe<ViewDisabledEvent>(OnViewDisabled);
        }

        public void Dispose()
        {
            DisposeAllControllers();

            EventBus.Unsubscribe<ViewEnabledEvent>(OnViewEnabled);
            EventBus.Unsubscribe<ViewDisabledEvent>(OnViewDisabled);
        }

        public TController GetController<TController>(View droneView) where TController : Controller
        {
            if (_controllers[droneView] is not TController controller)
            {
                throw new ArgumentException(
                    $"Controller of view {droneView.GetType().FullName} " +
                    $"is not of type {typeof(TController).FullName}!");
            }

            return controller;
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

        private static void DisposeController(Controller controller)
        {
            if (controller is IDisposable disposableController)
            {
                disposableController.Dispose();
            }
        }

        [CanBeNull]
        private Controller TryCreateControllerForView(View view)
        {
            var controllerType = GetControllerType(view);
            if (controllerType == null) return null;

            var resolvedConstructorParameters = ResolveControllerConstructorParameters(view, controllerType);
            return (Controller)Activator.CreateInstance(controllerType, args: resolvedConstructorParameters);
        }

        private object[] ResolveControllerConstructorParameters(View view, Type controllerType)
        {
            var constructorParameters = controllerType.GetConstructors().First().GetParameters();
            var resolvedConstructorParameters = new object[constructorParameters.Length];

            foreach (var parameter in constructorParameters)
            {
                resolvedConstructorParameters[parameter.Position] =
                    ResolveControllerConstructorParameter(view, controllerType, parameter);
            }

            return resolvedConstructorParameters;
        }

        private object ResolveControllerConstructorParameter(
            View view,
            Type controllerType,
            ParameterInfo parameter)
        {
            var parameterType = parameter.ParameterType;

            if (parameterType == view.GetType())
            {
                return view;
            }

            try
            {
                return _serviceRegistry.Get(parameterType);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException(
                    $"Cannot resolve parameter of type {parameterType.FullName} " +
                    $"during creation of controller {controllerType.FullName}. " +
                    "Make sure it is registered in the ServiceRegistry.",
                    exception);
            }
        }

        [CanBeNull]
        private static Type GetControllerType(View view)
        {
            var controllerType = ControllerUtility.GetAllControllerImplementationTypes()
                .FirstOrDefault(type => type.BaseType != null &&
                                        type.BaseType.GetGenericArguments()[0] == view.GetType());

            return controllerType;
        }
    }
}