using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Kandooz.KVR {
    [CustomEditor(typeof(HandAnimationController))]
    public class HandAnimationControllerEditor : Editor {
        public static readonly string[] FinerNames= { "Thumb", "Index", "Middle", "Ring","Pinky" };
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var fingers = serializedObject.FindProperty("fingers");
            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < fingers.arraySize; i++)
            {
                var fingerWeight=fingers.GetArrayElementAtIndex(i).FindPropertyRelative("weight");
                EditorGUILayout.PropertyField(fingerWeight, new GUIContent(FinerNames[i]));
            }
            serializedObject.ApplyModifiedProperties();
            if (EditorGUI.EndChangeCheck())
            {
                HandAnimationController controller = (HandAnimationController)target;
                controller.Index = controller.Index;
                controller.Thumb = controller.Thumb;
                controller.Ring = controller.Ring;
                controller.Middle = controller.Middle;
                controller.Pinky = controller.Pinky;
            }
        }
    }
}