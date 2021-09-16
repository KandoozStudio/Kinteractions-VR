using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Kandooz.KVR
{
    [CustomEditor(typeof(HandInputMapper))]
    public class HandInputMapperEditor : Editor
    {
        HandInputMapper mapper;
        bool folded = false;
        //HandAnimationController controller;
        void OnEnable()
        {
            mapper = (HandInputMapper)target;
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DisplayMapperEditor(serializedObject);
            serializedObject.ApplyModifiedProperties();

        }
        public void DisplayMapperEditor(SerializedObject serializedObject)
        {
            if (!mapper.InputManager)
            {
                EditorGUILayout.LabelField("Please select an input manager object or use default");
                mapper.InputManager = ScriptableObject.CreateInstance<UnityXRInputManager>();
                mapper.InputManager.name = "Default UnityXR Input manager";
            }
            folded = EditorGUILayout.BeginFoldoutHeaderGroup(folded, "current constraints");
            if (folded)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("constraints"), new GUIContent(""));
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("restore default constraints", GUILayout.Width(230), GUILayout.Height(25)))
                {
                    this.mapper.Constraints = HandConstraints.Free;
                    Undo.RegisterCompleteObjectUndo(mapper, "restored default constraints");
                }
                GUILayout.FlexibleSpace();

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            serializedObject.ApplyModifiedProperties();
        }
    }
}