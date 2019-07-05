//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;
//namespace Kandooz.KVR
//{
//    [CustomPropertyDrawer(typeof(Finger))]
//    public class FingerPropertDrawer : PropertyDrawer
//    {
//        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//        {
//            //base.OnGUI(position, property, label);
//            EditorGUI.BeginChangeCheck();
//            var weight = property.FindPropertyRelative("weight");
//            EditorGUI.PropertyField(position,weight);
//            if (EditorGUI.EndChangeCheck())
//            {
//                Finger f=(Finger)property.objectReferenceValue;
//                f.Weight = weight.floatValue;
//            };
//        }

//    }
//}