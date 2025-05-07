using UnityEditor;

namespace Utility.Extensions.Editor
{
    public static class SerializedObjectExtensions
    {
        public static SerializedProperty FindAutoProperty(this SerializedObject serializedObject, string propertyName)
        {
            return serializedObject.FindProperty($"<{propertyName}>k__BackingField");
        }
    }
}