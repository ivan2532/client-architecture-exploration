using UnityEditor;
using UnityEngine;
using Utility.Extensions.Editor;

namespace Features.Game.Configuration.Editor
{
    [CustomEditor(typeof(DroneConfiguration))]
    public class DroneConfigurationEditor : UnityEditor.Editor
    {
        private SerializedProperty _followSmoothTimeProperty;
        private SerializedProperty _lookSensitivityProperty;
        private SerializedProperty _minimumPitchProperty;
        private SerializedProperty _maximumPitchProperty;
        private SerializedProperty _minimumYawProperty;
        private SerializedProperty _maximumYawProperty;

        private void OnEnable()
        {
            _followSmoothTimeProperty =
                serializedObject.FindAutoProperty(nameof(DroneConfiguration.FollowSmoothTime));

            _lookSensitivityProperty = serializedObject.FindAutoProperty(nameof(DroneConfiguration.LookSensitivity));

            _minimumPitchProperty = serializedObject.FindAutoProperty(nameof(DroneConfiguration.MinimumPitch));
            _maximumPitchProperty = serializedObject.FindAutoProperty(nameof(DroneConfiguration.MaximumPitch));

            _minimumYawProperty = serializedObject.FindAutoProperty(nameof(DroneConfiguration.MinimumYaw));
            _maximumYawProperty = serializedObject.FindAutoProperty(nameof(DroneConfiguration.MaximumYaw));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawFields();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawFields()
        {
            DrawFollowOptions();
            DrawLookOptions();
        }

        private void DrawFollowOptions()
        {
            EditorGUILayout.PropertyField(_followSmoothTimeProperty);
        }

        private void DrawLookOptions()
        {
            EditorGUILayout.PropertyField(_lookSensitivityProperty);

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Pitch Range", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_minimumPitchProperty, new GUIContent("Minimum (Inclusive)"));
            EditorGUILayout.PropertyField(_maximumPitchProperty, new GUIContent("Maximum (Inclusive)"));

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Yaw Range", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_minimumYawProperty, new GUIContent("Minimum (Inclusive)"));
            EditorGUILayout.PropertyField(_maximumYawProperty, new GUIContent("Maximum (Inclusive)"));
        }
    }
}