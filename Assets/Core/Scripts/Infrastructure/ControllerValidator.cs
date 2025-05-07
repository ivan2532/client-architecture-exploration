using System;
using System.Linq;
using Core.Controller;
using UnityEditor;
using UnityEngine;

namespace Core.Infrastructure
{
    [InitializeOnLoad]
    public static class ControllerValidator
    {
        private static bool _lastValidationSuccessful;

        static ControllerValidator()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
            AssemblyReloadEvents.afterAssemblyReload += Validate;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode && !_lastValidationSuccessful)
            {
                EditorApplication.ExitPlaymode();
                ShowSceneViewNotification(
                    "Controller validation failed. Fix all errors before entering play mode.");
                Validate();
            }
        }

        private static void Validate()
        {
            _lastValidationSuccessful = true;

            var controllerTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var controllerType in controllerTypes)
            {
                _lastValidationSuccessful &= ValidateControllerConstructors(controllerType);
            }
        }

        private static bool ValidateControllerConstructors(Type controllerType)
        {
            var validationSuccessful = true;
            var constructors = controllerType.GetConstructors();

            if (constructors.Length != 1)
            {
                Debug.LogError(
                    $"Controller '{controllerType.FullName}' must have exactly one public constructor! " +
                    $"Found {constructors.Length}.");

                validationSuccessful = false;
            }

            var viewType = controllerType.BaseType!.GetGenericArguments()[0];
            var constructor = constructors.First();
            var viewParametersCount =
                constructor.GetParameters().Count(parameter => parameter.ParameterType == viewType);

            if (viewParametersCount != 1)
            {
                Debug.LogError(
                    $"Constructor of the controller '{controllerType.FullName}' " +
                    $"must have exactly one parameter of type {viewType.FullName}! " +
                    $"Found {viewParametersCount}.");

                validationSuccessful = false;
            }

            return validationSuccessful;
        }

        private static void ShowSceneViewNotification(string message)
        {
            var gameViewType = typeof(Editor).Assembly.GetType("UnityEditor.SceneView");
            if (gameViewType == null) return;

            var gameView = EditorWindow.GetWindow(gameViewType);
            gameView.ShowNotification(new GUIContent(message));
        }
    }
}