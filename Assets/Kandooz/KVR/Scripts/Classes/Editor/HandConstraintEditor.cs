using UnityEngine;
using UnityEditor;

namespace Kandooz.KVR {
    [CustomPropertyDrawer(typeof(HandConstrains))]
    public class HandConstraintEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float x = position.x;
            EditorGUI.BeginProperty(position, label, property);
            
            var pos = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            position.x = x;
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 1;
            var height = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("thumbFingerLimits")) ;
            
            var fingerPosition = new Rect(position.x, position.y+EditorGUIUtility.singleLineHeight, position.width, height);

            EditorGUI.PropertyField(fingerPosition, property.FindPropertyRelative("indexFingerLimits"));
            fingerPosition.y += height;

            EditorGUI.PropertyField(fingerPosition, property.FindPropertyRelative("middleFingerLimits"));           
            fingerPosition.y += height;

            EditorGUI.PropertyField(fingerPosition, property.FindPropertyRelative("ringFingerLimits"));
            fingerPosition.y += height;

            EditorGUI.PropertyField(fingerPosition, property.FindPropertyRelative("pinkyFingerLimits"));
            fingerPosition.y += height;

            EditorGUI.PropertyField(fingerPosition, property.FindPropertyRelative("thumbFingerLimits"));

            EditorGUI.EndProperty();
            EditorGUI.indentLevel = indent;
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("thumbFingerLimits")) * 5;
            height += EditorGUIUtility.singleLineHeight;
            return height;
        }

    }
}