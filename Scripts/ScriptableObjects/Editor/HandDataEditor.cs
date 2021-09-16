using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
namespace Kandooz.KVR
{
    [CustomEditor(typeof(HandData))]
    [CanEditMultipleObjects]
    public class HandDataEditor : Editor
    {
        HandData data;
        private void OnEnable()
        {
            data = (HandData)target;
        }
        public override void OnInspectorGUI()
        {

            DrawDefaultInspector();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Default Pose  animations");
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultPose").FindPropertyRelative("open"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultPose").FindPropertyRelative("closed"));
            EditorGUI.indentLevel--;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("poses"));
            serializedObject.ApplyModifiedProperties();
            data.defaultPose.Name = "default";

        }
    }
}