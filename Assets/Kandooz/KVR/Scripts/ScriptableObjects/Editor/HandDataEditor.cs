using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
namespace Kandooz.KVR
{
    [CustomEditor(typeof(HandData))]
    public class HandDataEditor : Editor
    {
        ReorderableList poses;
        void OnEnable()
        {
            poses = new ReorderableList(serializedObject, serializedObject.FindProperty("poses"), true, true, true, true);
            poses.drawHeaderCallback = (rect) => { EditorGUI.LabelField(rect, "Custom Poses"); };

            poses.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.height -= 2;
                rect.x += 2;
                var element = serializedObject.FindProperty("poses").GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, element, new GUIContent(""));
                
            };
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            poses.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}