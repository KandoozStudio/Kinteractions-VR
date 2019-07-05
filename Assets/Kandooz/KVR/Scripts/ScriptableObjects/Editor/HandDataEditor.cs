using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Kandooz.KVR
{
    [CustomEditor(typeof(HandData))]
    public class HandDataEditor : Editor
    {
        HandData handData;
        void OnEnable()
        {
            handData = (HandData)target;
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            serializedObject.ApplyModifiedProperties();
        }
    }
}