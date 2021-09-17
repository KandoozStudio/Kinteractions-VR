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
        static readonly string type = "type";
        static readonly string name = "name";
        static readonly string open = "open";
        static readonly string closed = "closed";


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var line = new Rect(position);
            line.height = EditorGUIUtility.singleLineHeight;
            line = DrawPropertyAndIncreaseheight(property, line, type);
            line = DrawPropertyAndIncreaseheight(property, line, name);

            EditorGUI.indentLevel++;
            if (IsStatic(property))
            {
                EditorGUI.PropertyField(line, property.FindPropertyRelative("open"), new GUIContent("animation clip"));

            }
            else
            {
                line = DrawPropertyAndIncreaseheight(property, line, open);
                line = DrawPropertyAndIncreaseheight(property, line, closed);

            }
            EditorGUI.indentLevel--;

        }

        private static Rect DrawPropertyAndIncreaseheight(SerializedProperty property, Rect line,string name)
        {
            EditorGUI.PropertyField(line, property.FindPropertyRelative(name));
            line.y += line.height;
            return line;
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
