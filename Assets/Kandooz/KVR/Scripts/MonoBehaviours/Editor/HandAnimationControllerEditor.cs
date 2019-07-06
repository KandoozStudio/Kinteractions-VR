using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Kandooz.KVR
{
    [CustomEditor(typeof(HandAnimationController))]
    public class HandAnimationControllerEditor : Editor
    {
        HandAnimationController controller;
        void OnEnable()
        {
            controller = (HandAnimationController)target;
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var fingers = serializedObject.FindProperty("fingers");

            EditorGUI.BeginChangeCheck();
            controller.StaticPose = EditorGUILayout.Toggle(new GUIContent("static Pose"), controller.StaticPose);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterFullObjectHierarchyUndo(controller, "Changed static pose");
            }
            if (controller.StaticPose)
            {
                var currentPose = serializedObject.FindProperty("pose");
                var poses = serializedObject.FindProperty("poses");
                string[] list = new string[poses.arraySize];
                for (int i = 0; i < list.Length; i++)
                {
                    list[i] = ((AnimationClip)poses.GetArrayElementAtIndex(i).FindPropertyRelative("clip").objectReferenceValue).name;
                }
                controller.Pose =
                    EditorGUILayout.Popup(new GUIContent("pose"), currentPose.intValue, list);
            }
            else
            {
                for (int i = 0; i < fingers.arraySize; i++)
                {
                    var fingerWeight = fingers.GetArrayElementAtIndex(i).FindPropertyRelative("weight");
                    EditorGUILayout.PropertyField(fingerWeight, new GUIContent(((FingerName)i).ToString()));
                }
                serializedObject.ApplyModifiedProperties();

                for (int i = 0; i < 5; i++)
                {
                    controller[i] = controller[i];

                }

            }
        }
    }
}