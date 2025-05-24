using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Core.Bootstrapping.Editor
{
    [InitializeOnLoad]
    public static class BootstrapSceneLoader
    {
        private const string BootstrapScenePath = "Assets/Core/Scenes/Bootstrap.unity";
        private const string PreviousSceneKey = "BootstrapSceneLoader_PreviousScenePath";

        static BootstrapSceneLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode) SaveCurrentSceneAndLoadBootstrapScene();
            else if (state == PlayModeStateChange.EnteredEditMode) LoadPreviousScene();
        }

        private static void SaveCurrentSceneAndLoadBootstrapScene()
        {
            var currentScenePath = SceneManager.GetActiveScene().path;
            EditorPrefs.SetString(PreviousSceneKey, currentScenePath);

            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorApplication.ExitPlaymode();
                return;
            }

            if (BootstrapScenePath != currentScenePath)
            {
                EditorSceneManager.OpenScene(BootstrapScenePath);
            }
        }

        private static void LoadPreviousScene()
        {
            if (!EditorPrefs.HasKey(PreviousSceneKey)) return;

            var previousScenePath = EditorPrefs.GetString(PreviousSceneKey);
            if (!string.IsNullOrEmpty(previousScenePath) && previousScenePath != BootstrapScenePath)
            {
                EditorSceneManager.OpenScene(previousScenePath);
            }
        }
    }
}