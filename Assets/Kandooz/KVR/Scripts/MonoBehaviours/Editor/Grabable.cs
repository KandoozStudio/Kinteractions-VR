//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;
//namespace Kandooz.KVR
//{
//    [CustomEditor(typeof(Grabable))]
//    public class GrabableEditor : Editor
//    {
//        Grabable grabable;
//        Transform leftPivot, rightPivot;
//        HandAnimationController currentlyControlledHand;
//        HandType hand;
//        bool editmode;
//        void OnEnable()
//        {
//            grabable = (Grabable)target;
//            grabable.Initialize();
//            editmode = false;
//            serializedObject.FindProperty("editMode").boolValue = false;
//            leftPivot = (Transform)serializedObject.FindProperty("leftPivot").objectReferenceValue;
//            rightPivot = (Transform)serializedObject.FindProperty("rightPivot").objectReferenceValue;

//        }
//        //public override void OnInspectorGUI()
//        //{
//        //    base.OnInspectorGUI();
//        //    if (serializedObject.FindProperty("editMode").boolValue)
//        //    {
//        //        var hand = serializedObject.FindProperty("handToEdit");
//        //        EditorGUILayout.PropertyField(hand);
//        //        serializedObject.ApplyModifiedProperties();
//        //        if (hand.enumValueIndex != (int)this.hand || !currentlyControlledHand)
//        //        {
//        //            End();
//        //            this.hand = (HandType)hand.enumValueIndex;
//        //            HandState handState = grabable.RightHand;
//        //            switch (this.hand)
//        //            {
//        //                case HandType.right:
//        //                    handState = grabable.RightHand;
//        //                    currentlyControlledHand = GameObject.Instantiate(grabable.data.rightHandPrefab, Vector3.zero, Quaternion.identity, rightPivot);
//        //                    currentlyControlledHand.transform.localPosition = Vector3.zero;
//        //                    currentlyControlledHand.transform.localRotation = Quaternion.identity;
//        //                    break;
//        //                case HandType.left:
//        //                    handState = grabable.LeftHand;
//        //                    currentlyControlledHand = GameObject.Instantiate(grabable.data.leftHandPrefab, Vector3.zero, Quaternion.identity, leftPivot);
//        //                    currentlyControlledHand.transform.localPosition = Vector3.zero;
//        //                    currentlyControlledHand.transform.localRotation = Quaternion.identity;

//        //                    break;
//        //            }
//        //            currentlyControlledHand.name = "hand";
//        //            currentlyControlledHand.Init();


//        //            currentlyControlledHand.StaticPose = handState.staticPose;
//        //            currentlyControlledHand.Pose = handState.pose;
//        //            for (int i = 0; i < 5; i++)
//        //            {
//        //                currentlyControlledHand[i] = handState.fingers[i];
//        //            }
//        //        }
//        //        SerializedObject so = new SerializedObject(currentlyControlledHand);
//        //        HandAnimationControllerEditor.DisplayHandEditor(currentlyControlledHand, so);

//        //    }
//        //    else
//        //    {
//        //        End();
//        //    }
//        //}
//        void OnDisable()
//        {
//            End();
//            Tools.hidden = false;
//        }

//        void End()
//        {
//            if (currentlyControlledHand)
//            {
//                var fingers = new float[5];
//                for (int i = 0; i < fingers.Length; i++)
//                {
//                    fingers[i] = currentlyControlledHand[i];
//                }
//                HandState state = new HandState(currentlyControlledHand.Pose, currentlyControlledHand.StaticPose, fingers);
//                switch (hand)
//                {
//                    case HandType.right:
//                        grabable.RightHand = state;
//                        break;
//                    case HandType.left:
//                        grabable.LeftHand = state;
//                        break;
//                }
//                GameObject.DestroyImmediate(currentlyControlledHand.gameObject);
//            }
//            currentlyControlledHand = null;
//        }
//        void OnSceneGUI()
//        {
//            if (currentlyControlledHand)
//            {
//                Tools.hidden = true;
//                var parent = currentlyControlledHand.transform.parent;
//                EditorGUI.BeginChangeCheck();
//                var deltaRotation = Handles.DoRotationHandle(parent.localRotation, parent.position);
//                parent.localRotation = deltaRotation;
//                if (EditorGUI.EndChangeCheck())
//                {
//                    Undo.RegisterFullObjectHierarchyUndo(parent, "Rotated " + parent.name);
//                }
//                EditorGUI.BeginChangeCheck();
//                var deltaPosition = Handles.DoPositionHandle(parent.position, parent.rotation);
//                parent.position = deltaPosition;
//                if (EditorGUI.EndChangeCheck())
//                {
//                    Undo.RegisterFullObjectHierarchyUndo(parent, "moved" + parent.name);
//                }
//            }
//            else
//            {
//                Tools.hidden = false;
//            }

//        }
//    }
//}