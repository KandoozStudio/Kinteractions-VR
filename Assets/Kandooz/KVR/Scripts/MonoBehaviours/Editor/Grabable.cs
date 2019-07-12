using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Kandooz.KVR {
    [CustomEditor(typeof(Grabable))]
    public class GrabableEditor : Editor
    {
        Grabable grabable;
        void OnEnable()
        {
            grabable = (Grabable)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var editmode =serializedObject.FindProperty("editMode").boolValue;
            var pivot = (GameObject)serializedObject.FindProperty("handPosition").objectReferenceValue;
            var child = pivot.transform.Find("hand");

            if (editmode)
            {
                if (!child)
                {
                    GameObject.Instantiate(grabable.data.HandPrefab,pivot.transform);
                }
            }
            else
            {

            }
        }
    }
}