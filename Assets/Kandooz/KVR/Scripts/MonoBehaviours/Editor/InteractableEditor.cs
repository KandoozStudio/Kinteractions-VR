﻿using UnityEngine;
using UnityEditor;
namespace Kandooz.KVR
{

    enum HandtoEdit
    {
        none, right, left
    }
    [CustomEditor(typeof(Interactable))]
    public class InteractableEditor : Editor
    {
        HandAnimationController visibleHand;
        private bool showEvents = false;
        HandtoEdit currentHand = HandtoEdit.none;
        private Interactable interactable;


        private void OnEnable()
        {
            Tools.hidden = false;

            interactable = (Interactable)target;
            EditorApplication.update += Update;
            if (visibleHand)
            {
                GameObject.DestroyImmediate(visibleHand.gameObject);
                currentHand = HandtoEdit.none;
            }
            else if (visibleHand = interactable.GetComponentInChildren<HandAnimationController>())
            {
                GameObject.DestroyImmediate(visibleHand.gameObject);
                visibleHand = null;
            }

            if (!interactable.handData)
            {
                interactable.handData = GameObject.FindObjectOfType<HandAnimationController>().HandData;
            }


        }
        private void OnDisable()
        {
            EditorApplication.update -= Update;

            if (visibleHand)
                GameObject.DestroyImmediate(visibleHand.gameObject);
            currentHand = HandtoEdit.none;
            Tools.hidden = false;

        }
        private void OnDestroy()
        {
            if (visibleHand)
                GameObject.DestroyImmediate(visibleHand.gameObject);
            currentHand = HandtoEdit.none;
            Tools.hidden = false;

        }

        private void OnSceneGUI()
        {
            if (visibleHand)
            {
                Tools.hidden = true;
                EditorGUI.BeginChangeCheck();

                var deltaRotation = Handles.DoRotationHandle(visibleHand.transform.localRotation, visibleHand.transform.position);
                var deltaPosition = Handles.PositionHandle(visibleHand.transform.position, visibleHand.transform.localRotation);
                visibleHand.transform.position= deltaPosition;
                visibleHand.transform.localRotation = deltaRotation;
                switch (currentHand)
                {
                    case HandtoEdit.none:
                        break;
                    case HandtoEdit.right:
                        interactable.rightHandPivot.position = visibleHand.transform.localPosition;
                        interactable.rightHandPivot.rotation = visibleHand.transform.localEulerAngles;
                        break;
                    case HandtoEdit.left:
                        interactable.leftHandPivot.position = visibleHand.transform.localPosition;
                        interactable.leftHandPivot.rotation = visibleHand.transform.localEulerAngles;

                        break;
                    default:
                        break;
                }
            }
            else
            {
                Tools.hidden = false;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
            {
                if(visibleHand)
                    DestroyImmediate(visibleHand);
            }
            else if (interactable.handData)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("constraintHandOnHover"));
                if (serializedObject.FindProperty("constraintHandOnHover").boolValue)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginVertical("GroupBox");

                    EditorGUILayout.PropertyField(serializedObject.FindProperty("hoverHandConstraints"));
                    EditorGUILayout.EndVertical();
                    EditorGUI.indentLevel--;


                }
                EditorGUILayout.PropertyField(serializedObject.FindProperty("constraintHandOnInteraction"));

                if (serializedObject.FindProperty("constraintHandOnInteraction").boolValue)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Edit Right hand"))
                    {
                        if (currentHand == HandtoEdit.right)
                        {
                            currentHand = HandtoEdit.none;
                        }
                        else
                        {
                            if (visibleHand)
                            {
                                DestroyImmediate(visibleHand.gameObject);
                            }
                            currentHand = HandtoEdit.right;
                        }
                    }
                    if (GUILayout.Button("Edit Left hand"))
                    {
                        if (currentHand == HandtoEdit.left)
                        {
                            currentHand = HandtoEdit.none;
                        }
                        else
                        {
                            if (visibleHand)
                            {
                                DestroyImmediate(visibleHand.gameObject);
                            }

                            currentHand = HandtoEdit.left;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    if (currentHand != HandtoEdit.none)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.BeginVertical("GroupBox");
                        switch (currentHand)
                        {
                            case HandtoEdit.none:
                                break;
                            case HandtoEdit.right:
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("rightHandLimits"));
                                EditorGUILayout.Space();
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("rightHandPivot").FindPropertyRelative("position"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("rightHandPivot").FindPropertyRelative("rotation"));
                                break;
                            case HandtoEdit.left:
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("leftHandLimits"));
                                EditorGUILayout.Space();

                                EditorGUILayout.PropertyField(serializedObject.FindProperty("leftHandPivot").FindPropertyRelative("position"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("leftHandPivot").FindPropertyRelative("rotation"));
                                break;
                            default:
                                break;
                        }
                        EditorGUILayout.EndVertical();
                        EditorGUI.indentLevel--;
                        serializedObject.ApplyModifiedProperties();
                        HandPositionSceneEditor(currentHand);
                    }
                    else
                    {
                        if (visibleHand) GameObject.DestroyImmediate(visibleHand.gameObject);
                    }
                }

                serializedObject.ApplyModifiedProperties();
            }
            showEvents = EditorGUILayout.Foldout(showEvents, "Interaction Events");
            if (showEvents)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onHandHoverStart"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onHandHoverEnd"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onInteractionStart"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onInteractionEnd"));

            }
        }

        private void HandPositionSceneEditor(HandtoEdit hand)
        {
            if (visibleHand == null)
            {
                switch (hand)
                {
                    case HandtoEdit.none:
                        break;
                    case HandtoEdit.right:
                        visibleHand = GameObject.Instantiate<HandAnimationController>(interactable.handData.rightHandPrefab);
                        break;
                    case HandtoEdit.left:
                        visibleHand = GameObject.Instantiate<HandAnimationController>(interactable.handData.leftHandPrefab);
                        break;
                    default:
                        break;
                }
            }
            visibleHand.transform.parent = interactable.transform;

            switch (hand)
            {
                case HandtoEdit.none:
                    break;
                case HandtoEdit.right:
                    visibleHand.transform.localPosition = interactable.rightHandPivot.position;
                    visibleHand.transform.localRotation = Quaternion.Euler(interactable.rightHandPivot.rotation);

                    break;
                case HandtoEdit.left:
                    visibleHand.transform.localPosition = interactable.leftHandPivot.position;
                    visibleHand.transform.localRotation = Quaternion.Euler(interactable.leftHandPivot.rotation);
                    break;
                default:
                    break;
            }

        }


        float t = 0;
        void Update()
        {
            if (t > 1)
            {
                t = 0;
            }
            t += .01f;
            if (visibleHand)
            {
                if (!EditorApplication.isPlaying)
                {
                    if (visibleHand.graph.IsValid())
                    {

                        visibleHand.Update();
                    }
                    else
                    {
                        visibleHand.Init();
                    }

                    var constraints = (currentHand == HandtoEdit.left) ? interactable.LeftHandLimits : interactable.RightHandLimits;

                    visibleHand[FingerName.Index] = Mathf.Lerp(constraints.indexFingerLimits.min, constraints.indexFingerLimits.max, t);
                    visibleHand[FingerName.Middle] = Mathf.Lerp(constraints.middleFingerLimits.min, constraints.middleFingerLimits.max, t);
                    visibleHand[FingerName.Ring] = Mathf.Lerp(constraints.ringFingerLimits.min, constraints.ringFingerLimits.max, t);
                    visibleHand[FingerName.Pinky] = Mathf.Lerp(constraints.pinkyFingerLimits.min, constraints.pinkyFingerLimits.max, t);
                    visibleHand[FingerName.Thumb] = Mathf.Lerp(constraints.thumbFingerLimits.min, constraints.thumbFingerLimits.max, t);
                }
            }
        }
    }
}