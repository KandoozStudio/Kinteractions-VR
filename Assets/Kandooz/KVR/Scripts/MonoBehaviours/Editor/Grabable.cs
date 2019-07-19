using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Kandooz.KVR {
    [CustomEditor(typeof(Grabable))]
    public class GrabableEditor : Editor
    {
        Grabable grabable;
        Transform leftPivot, rightPivot;
        HandAnimationController currentlyControlledHand;
        Hand hand;
        bool editmode;
        void OnEnable()
        {
            grabable = (Grabable)target;
            grabable. Initialize();
            editmode = false;
            serializedObject.FindProperty("editMode").boolValue = false; ;

            leftPivot = (Transform)serializedObject.FindProperty("leftPivot").objectReferenceValue;
            rightPivot= (Transform)serializedObject.FindProperty("rightPivot").objectReferenceValue;
            
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (serializedObject.FindProperty("editMode").boolValue)
            {
                var hand = serializedObject.FindProperty("handToEdit");
                EditorGUILayout.PropertyField(hand);
                Debug.Log(hand.enumValueIndex );
                serializedObject.ApplyModifiedProperties();
                if (hand.enumValueIndex != (int)this.hand ||!currentlyControlledHand)
                {
                    this.hand = (Hand)hand.enumValueIndex;
                    if (currentlyControlledHand) 
                        GameObject.DestroyImmediate(currentlyControlledHand.gameObject);
                    Debug.Log(this.hand.ToString());
                    switch (this.hand)
                    {
                        case Hand.right:
                            currentlyControlledHand = GameObject.Instantiate(grabable.data.rightHandPrefab,Vector3.zero,Quaternion.identity,rightPivot);
                            currentlyControlledHand.transform.localPosition = Vector3.zero;
                            currentlyControlledHand.transform.localRotation= Quaternion.identity;
                            break;
                        case Hand.left:
                            currentlyControlledHand = GameObject.Instantiate(grabable.data.leftHandPrefab, Vector3.zero, Quaternion.identity, leftPivot);
                            currentlyControlledHand.transform.localPosition = Vector3.zero;
                            currentlyControlledHand.transform.localRotation = Quaternion.identity;

                            break;
                    }
                    currentlyControlledHand.name = "hand";
                }
                SerializedObject so = new SerializedObject(currentlyControlledHand);
                HandAnimationControllerEditor.DisplayHandEditor(currentlyControlledHand,so);

            }
            else
            {
                if (currentlyControlledHand)
                    GameObject.DestroyImmediate(currentlyControlledHand.gameObject);
                currentlyControlledHand = null;
            }
        }
        void OnDisable()
        {
            if (currentlyControlledHand)
                GameObject.DestroyImmediate(currentlyControlledHand.gameObject);
            currentlyControlledHand = null;
            Tools.hidden = false;

        }
        void OnSceneGUI()
        {
            if (currentlyControlledHand)
            {
                Tools.hidden = true;
                var parent = currentlyControlledHand.transform.parent;
                EditorGUI.BeginChangeCheck();
                var deltaRotation = Handles.DoRotationHandle(parent.localRotation, parent.position);
                Debug.Log(deltaRotation.eulerAngles);
                parent.localRotation = deltaRotation;
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RegisterFullObjectHierarchyUndo(parent, "Rotated " + parent.name);
                }

                EditorGUI.BeginChangeCheck();
                var deltaPosition = Handles.DoPositionHandle(parent.position,parent.rotation);
                
                parent.position= deltaPosition;
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RegisterFullObjectHierarchyUndo(parent, "moved" + parent.name);
                }
                
            }
            else
            {
                Tools.hidden = false;
            }

        }
    }
}