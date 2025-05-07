using System;
using System.Collections.Generic;
using System.Linq;
using Core.Controller;
using Core.View;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.Extensions;
using Object = UnityEngine.Object;

namespace Core.Infrastructure
{
    public class ControllerService : IDisposable
    {
        private readonly Dictionary<ViewBase, ControllerBase> _controllers = new();

        public ControllerService()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Dispose()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneUnloaded(Scene scene)
        {
            DisposeLocalControllersFromUnloadedScene();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            CreateControllersFromLoadedScene();
        }

        private void DisposeLocalControllersFromUnloadedScene()
        {
            var disposedControllerViews = new List<ViewBase>();

            foreach (var (view, controller) in _controllers)
            {
                if (view.gameObject.IsMarkedAsDontDestroyOnLoad()) continue;

                if (controller is IDisposable disposableController)
                {
                    disposableController.Dispose();
                }

                disposedControllerViews.Add(view);
            }

            foreach (var disposedControllerView in disposedControllerViews)
            {
                _controllers.Remove(disposedControllerView);
            }
        }

        private void CreateControllersFromLoadedScene()
        {
            var views = Object.FindObjectsByType<ViewBase>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (var view in views.Where(view => !_controllers.ContainsKey(view)))
            {
                var controller = TryCreateControllerForView(view);
                if (controller != null) _controllers.Add(view, controller);
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