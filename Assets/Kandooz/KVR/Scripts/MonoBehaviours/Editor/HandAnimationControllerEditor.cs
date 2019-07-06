using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Kandooz.KVR {
    [CustomEditor(typeof(HandAnimationController))]
    public class HandAnimationControllerEditor : Editor {
        HandAnimationController controller;
        void OnEnable()
        {
            controller = (HandAnimationController)target;
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var fingers = serializedObject.FindProperty("fingers");
            Debug.Log(fingers);
            EditorGUI.BeginChangeCheck();
            controller.StaticPose=EditorGUILayout.Toggle(new GUIContent("static Pose"), controller.StaticPose);
            if (EditorGUI.EndChangeCheck()) {
                Undo.RegisterFullObjectHierarchyUndo(controller,"Changed static pose");
            }
            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < fingers.arraySize; i++)
            {
                var fingerWeight=fingers.GetArrayElementAtIndex(i).FindPropertyRelative("weight");
                EditorGUILayout.PropertyField(fingerWeight, new GUIContent(((FingerName)i).ToString()));
            }
            serializedObject.ApplyModifiedProperties();
            if (EditorGUI.EndChangeCheck())
            {
                for (int i = 0; i < 5; i++)
                {
                    controller[i] = controller[i];

                }
            }
        }
    }
}