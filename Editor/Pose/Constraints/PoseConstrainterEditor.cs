using System;
using Kandooz.InteractionSystem.Animations;
using Kandooz.InteractionSystem.Core;
using Kandooz.InteractionSystem.Interactions;
using Kinteractions_VR.Core.Runtime.Hand;
using UnityEditor;
using UnityEngine;
using InteractionPoseConstrainer = Kandooz.InteractionSystem.Interactions.InteractionPoseConstrainer;

namespace Kinteractions_VR.Interactions.Editors
{
    [CustomEditor(typeof(InteractionPoseConstrainer), true)]
    public class PoseConstrainterEditor : Editor
    {
        private InteractionPoseConstrainer interactable;
        private HandPoseController currentHand;
        private HandPoseController leftHandPrefab, rightHandPrefab;
        private HandIdentifier selectedHand;
        private CameraRig cameraRig;
        private float t = 0;

        private Transform Pivot
        {
            get
            {
                return selectedHand switch
                {
                    HandIdentifier.Left => interactable.LeftHandTransform,
                    HandIdentifier.Right => interactable.RightHandTransform,
                    _ => null
                };
            }
        }

        private HandIdentifier SelectedHand
        {
            set
            {
                selectedHand = value;
                DestroyOldHands();
                switch (selectedHand)
                {
                    case HandIdentifier.Left:
                        CreateHandInPivot(interactable.LeftHandTransform);
                        break;
                    case HandIdentifier.Right:
                        CreateHandInPivot(interactable.RightHandTransform);
                        break;
                }
            }
            get => selectedHand;
        }

        private void DestroyOldHands()
        {
            var rightHandInteractor = interactable.RightHandTransform.GetComponentInChildren<HandPoseController>();
            var leftHandInteractor = interactable.LeftHandTransform.GetComponentInChildren<HandPoseController>();
            if (currentHand) DestroyImmediate(currentHand.gameObject);
            if (rightHandInteractor != null) DestroyImmediate(rightHandInteractor.gameObject);
            if (leftHandInteractor != null) DestroyImmediate(leftHandInteractor.gameObject);
        }

        private void CreateHandInPivot(Transform pivot)
        {
            var hand = (selectedHand == HandIdentifier.Left) ? leftHandPrefab : rightHandPrefab;
            currentHand = Instantiate(hand).GetComponent<HandPoseController>();
            currentHand.gameObject.transform.localScale = Vector3.one;
            currentHand.gameObject.transform.parent = pivot;
            currentHand.gameObject.transform.localPosition = Vector3.zero;
            currentHand.gameObject.transform.localRotation = Quaternion.identity;
            currentHand.Initialize();
        }

        private void OnEnable()
        {
            interactable = (InteractionPoseConstrainer)target;
            InitializeHandPivot();
            selectedHand = HandIdentifier.None;
            cameraRig = FindAnyObjectByType<CameraRig>();
            if (cameraRig)
            {
                leftHandPrefab = (HandPoseController)cameraRig.LeftHandPrefab;
                rightHandPrefab = (HandPoseController)cameraRig.RightHandPrefab;
            }

            EditorApplication.update += () => t += .1f;
        }

        private void OnDisable()
        {
            SelectedHand = HandIdentifier.None;
            Tools.hidden = false;
            DestroyOldHands();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            HandleHandSelection();
            ShowPoseInspector();
            SetPose();
        }

        private void OnSceneGUI()
        {
            t += .1f;
            if (!currentHand)
            {
                Tools.hidden = false;
                return;
            }

            Tools.hidden = true;
            var transform = Pivot;
            var position = transform.position;
            var rotation = transform.rotation;
            Handles.TransformHandle(ref position, ref rotation);
            transform.position = position;
            transform.rotation = rotation;
        }


        private void InitializeHandPivot()
        {
            if (interactable.RightHandTransform == null)
            {
                interactable.RightHandTransform = CreateHandPivot("RightHandPivot");
            }

            if (interactable.LeftHandTransform == null)
            {
                interactable.LeftHandTransform = CreateHandPivot("LeftHandPivot");
            }
        }

        private Transform CreateHandPivot(string pivotName)
        {
            var handTransform = new GameObject(pivotName).transform;
            handTransform.parent = interactable.transform;
            handTransform.localPosition = Vector3.zero;
            handTransform.localRotation = Quaternion.identity;
            return handTransform;
        }

        private void ShowPoseInspector()
        {
            var propertyName = selectedHand switch
            {
                HandIdentifier.Left => "leftConstraints",
                HandIdentifier.Right => "rightConstraints",
                _ => ""
            };
            if (propertyName == "") return;
            EditorGUILayout.PropertyField(serializedObject.FindProperty(propertyName).FindPropertyRelative("poseConstraints"));
            serializedObject.ApplyModifiedProperties();
        }

        private void HandleHandSelection()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("RightHand Constraints"))
            {
                SelectHand(HandIdentifier.Right);
            }

            if (GUILayout.Button("LeftHand Constraints"))
            {
                SelectHand(HandIdentifier.Left);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void SelectHand(HandIdentifier hand)
        {
            if (selectedHand != hand)
            {
                SelectedHand = hand;
            }
            else
            {
                selectedHand = hand;
            }
        }

        private void SetPose()
        {
            var t = Mathf.PingPong(this.t, 1);
            if (selectedHand == HandIdentifier.None) return;
            var handConstraints = selectedHand == HandIdentifier.Left ? interactable.LeftPoseConstraints : interactable.RightPoseConstraints;
            for (int i = 0; i < 5; i++)
            {
                currentHand[i] = handConstraints[i].GetConstrainedValue(t);
            }

            currentHand.UpdateGraphVariables();
        }
    }
}