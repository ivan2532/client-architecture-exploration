using UnityEditor;
using UnityEngine;
using Utility.Extensions.Editor;

namespace Features.Game.Configuration.Editor
{
    [CustomEditor(typeof(GameConfiguration))]
    public class GameConfigurationEditor : UnityEditor.Editor
    {
        private SerializedProperty _lookSensitivityProperty;
        private SerializedProperty _minimumPitchProperty;
        private SerializedProperty _maximumPitchProperty;
        private SerializedProperty _minimumYawProperty;
        private SerializedProperty _maximumYawProperty;

        private void OnEnable()
        {
            _lookSensitivityProperty = serializedObject.FindAutoProperty(nameof(GameConfiguration.LookSensitivity));
            _minimumPitchProperty = serializedObject.FindAutoProperty(nameof(GameConfiguration.MinimumPitch));
            _maximumPitchProperty = serializedObject.FindAutoProperty(nameof(GameConfiguration.MaximumPitch));
            _minimumYawProperty = serializedObject.FindAutoProperty(nameof(GameConfiguration.MinimumYaw));
            _maximumYawProperty = serializedObject.FindAutoProperty(nameof(GameConfiguration.MaximumYaw));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawFields();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawFields()
        {
            DrawLookSensitivity();
            DrawPitch();
            DrawYaw();
        }

        private void DrawLookSensitivity()
        {
            EditorGUILayout.PropertyField(_lookSensitivityProperty);
        }

        private void DrawPitch()
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Pitch Range", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_minimumPitchProperty, new GUIContent("Minimum (Inclusive)"));
            EditorGUILayout.PropertyField(_maximumPitchProperty, new GUIContent("Maximum (Inclusive)"));
        }

        private void DrawYaw()
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Yaw Range", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_minimumYawProperty, new GUIContent("Minimum (Inclusive)"));
            EditorGUILayout.PropertyField(_maximumYawProperty, new GUIContent("Maximum (Inclusive)"));
        }
    }
}