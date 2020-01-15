// NOTE put in a Editor folder

using UnityEngine;
using UnityEditor;

namespace Kandooz.KVR
{
    [CustomPropertyDrawer(typeof(FingerConstraints))]
    public class MinMaxDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var pos = position;
            pos.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(pos, label);
            EditorGUI.indentLevel++;
            pos.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(pos, property.FindPropertyRelative("locked"));
            pos.y += EditorGUIUtility.singleLineHeight;
            pos.x = position.x;
            pos.width = position.width;
            if (property.FindPropertyRelative("locked").boolValue)
            {
                DrawNormalSlider(pos, property);
            }
            else
            {
                DrawMinMaxSlider(pos, property);
            }
            EditorGUI.indentLevel--;

        }

        private void DrawMinMaxSlider(Rect position, SerializedProperty property)
        {
            var labelPosition = position;
            labelPosition.x += position.width / 4 * 3 + 10;
            labelPosition.width /= 4;
            labelPosition.width -= 5;

            var sliderPosition = position;
            sliderPosition.width *= 3f / 4;
            sliderPosition.width -= 5;
            float minValue = property.FindPropertyRelative("x").floatValue;
            float maxValue = property.FindPropertyRelative("y").floatValue;
            float minLimit = 0;
            float maxLimit = 1;
            EditorGUI.MinMaxSlider(sliderPosition, "limits", ref minValue, ref maxValue, minLimit, maxLimit);
            EditorGUI.LabelField(labelPosition, minValue.ToString("0.0") + " : " + maxValue.ToString("0.0")); ;

            property.FindPropertyRelative("x").floatValue = minValue;
            property.FindPropertyRelative("y").floatValue = maxValue;
        }
        private void DrawNormalSlider(Rect position, SerializedProperty property)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("x"),new GUIContent("value"));
            property.FindPropertyRelative("y").floatValue = property.FindPropertyRelative("x").floatValue;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float size = EditorGUIUtility.singleLineHeight*3;
            return size;
        }
    }
}