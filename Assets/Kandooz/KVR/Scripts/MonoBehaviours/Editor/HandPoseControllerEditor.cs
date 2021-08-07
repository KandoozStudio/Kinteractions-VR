using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Kandooz.KVR
{
    [CustomEditor(typeof(HandPoseController))]
    public class HandAnimationControllerEditor : Editor
    {
        HandPoseController controller;
        //HandAnimationController controller;
        void OnEnable()
        {
            controller = (HandPoseController)target;
            if (controller.HandData)
            {
                controller.Initialize();
            }
            EditorApplication.update += Update;
            controller.Initialize();
        }
        void OnDisable()
        {
            EditorApplication.update -= Update;
        }
        void Update()
        {
            if (!EditorApplication.isPlaying)
            {
                if (controller.Graph.IsValid())
                {

                    controller.Update();
                }
                else
                {
                    controller.Initialize();
                }

            }
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DisplayHandEditor(serializedObject);
            serializedObject.ApplyModifiedProperties();

        }
        public void DisplayHandEditor(SerializedObject serializedObject)
        {

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("handData"));
            serializedObject.ApplyModifiedProperties();
            if (EditorGUI.EndChangeCheck())
            {
                controller.Initialize();
            }
            DrawCurrentPoseEditor(serializedObject);
            if (IsStaticPose())
            {
                DrawFingerEditor(serializedObject);
            }
        }
        private bool IsStaticPose()
        {
            var currentPose = controller.Poses[controller.Pose];
            var isStatic = currentPose.GetType() != typeof(StaticPose);
            return isStatic;
        }
        private void DrawCurrentPoseEditor(SerializedObject serializedObject)
        {
            var currentPose = serializedObject.FindProperty("pose");
            var poses = controller.Poses;
            string[] list = new string[poses.Count];
            for (int i = 0; i < list.Length; i++)
            {

                list[i] = poses[i].Name;
            }
            controller.Pose =
                EditorGUILayout.Popup(new GUIContent("pose"), currentPose.intValue, list);
        }
        private static void DrawFingerEditor(SerializedObject serializedObject)
        {
            var fingers = serializedObject.FindProperty("fingers");
            for (int i = 0; i < fingers.arraySize; i++)
            {
                var fingerWeight = fingers.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(fingerWeight, new GUIContent(((FingerName)i).ToString()));
            }
        }
    }
}