using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
namespace Kandooz.ScriptableSystem.Editors
{
    [CustomEditor(typeof(EventListner))]
    public class EventListnerEditor : UnityEditor.Editor
    {
        ReorderableList events;
        int selected = 0;

        private void OnEnable()
        {
            events = new ReorderableList(serializedObject,serializedObject.FindProperty("events"));
            events.drawElementCallback = DrawElementCallback;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            events.DoLayoutList();
            selected = events.index;
            if (selected >= 0)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("events").GetArrayElementAtIndex(selected).FindPropertyRelative("unityEvent"));
            }
            serializedObject.ApplyModifiedProperties();
        }
        void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused) {
            EditorGUI.PropertyField(rect, serializedObject.FindProperty("events").GetArrayElementAtIndex(index).FindPropertyRelative("scriptableEvent"),new GUIContent("event"));
        }
    }
}