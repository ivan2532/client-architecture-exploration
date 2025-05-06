using System;
using System.Collections.Generic;
using System.Linq;
using Core.Controller;
using Core.Utility;
using Core.View;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Core.Infrastructure
{
    public class ControllerManager : MonoBehaviour
    {
        private readonly Dictionary<ViewBase, ControllerBase> _controllers = new();

        private void Awake()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
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
            var views = FindObjectsByType<ViewBase>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (var view in views)
            {
                if (!_controllers.ContainsKey(view))
                {
                    _controllers.Add(view, CreateControllerForView(view));
                }
            }
        }

        private static ControllerBase CreateControllerForView(ViewBase view)
        {
            var controllerType = GetControllerType(view);
            return (ControllerBase)Activator.CreateInstance(controllerType, args: view);
        }

        private static Type GetControllerType(ViewBase view)
        {
            var controllerType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.BaseType != null && type.IsSubclassOf(typeof(ControllerBase)))
                .Where(type => type.BaseType.GetGenericTypeDefinition() == typeof(ControllerBase<>))
                .FirstOrDefault(type => type.BaseType.GetGenericArguments()[0] == view.GetType());

            if (controllerType == null)
            {
                throw new ArgumentException(
                    $"Could not find controller type for view of type {view.GetType().FullName}.", nameof(view));
            }

            return controllerType;
        }
    }
}