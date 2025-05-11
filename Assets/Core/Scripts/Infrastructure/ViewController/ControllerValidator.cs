using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core.Infrastructure.ViewController
{
    [InitializeOnLoad]
    public static class ControllerValidator
    {
        private static bool _lastValidationSuccessful;

        static ControllerValidator()
        {
            AssemblyReloadEvents.afterAssemblyReload += Validate;
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !_lastValidationSuccessful)
            {
                EditorApplication.ExitPlaymode();
                Debug.LogError("Controller validation failed. Play mode was blocked.");
                ShowSceneViewNotification("Fix controller validation errors before entering play mode.");
            }
        }

        private static void Validate()
        {
            _lastValidationSuccessful = true;

            var controllerTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(Controller).IsAssignableFrom(t) && !t.IsAbstract);

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