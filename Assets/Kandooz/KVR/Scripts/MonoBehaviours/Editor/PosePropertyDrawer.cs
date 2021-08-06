using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Kandooz.KVR
{
    [CustomPropertyDrawer(typeof(PoseData))]
    public class PosePropertyDrawer : PropertyDrawer
    {
        
    
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var line = new Rect(position);
            line.height = EditorGUIUtility.singleLineHeight;
            EditorGUI. PropertyField(line,property.FindPropertyRelative("type"));
            EditorGUI.indentLevel++;
            line.y += line.height;
            if (IsStatic(property))
            {
                EditorGUI.PropertyField(line, property.FindPropertyRelative("open"), new GUIContent("animation clip"));

            }
            else
            {
                EditorGUI.PropertyField(line, property.FindPropertyRelative("open"));
                line.y += line.height;
                EditorGUI.PropertyField(line, property.FindPropertyRelative("closed"));

            }
            EditorGUI.indentLevel--;
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsStatic(property))
            {
                return EditorGUIUtility.singleLineHeight * 3;
            }
            return EditorGUIUtility.singleLineHeight * 4; 
        }

        private bool IsStatic(SerializedProperty property)
        {
            return property.FindPropertyRelative("type").enumValueIndex == 0;
        }
    }

}
