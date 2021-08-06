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
            //poses.DoLayoutList();
            //serializedObject.ApplyModifiedProperties();
        }
    }
}