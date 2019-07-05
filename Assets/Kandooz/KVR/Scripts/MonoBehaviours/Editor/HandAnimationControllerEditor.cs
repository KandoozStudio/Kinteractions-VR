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
                controller[FingerName.Thumb] = controller[FingerName.Thumb];
                controller[FingerName.Index] = controller[FingerName.Index];
                controller[FingerName.Middle] = controller[FingerName.Middle];
                controller[FingerName.Ring] = controller[FingerName.Ring];
                controller[FingerName.Pinky] = controller[FingerName.Pinky];
            }
        }
    }
}