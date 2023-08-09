using System;
using Kandooz.InteractionSystem.Animations;
using Kandooz.InteractionSystem.Core;
using Kandooz.InteractionSystem.Interactions;
using Kinteractions_VR.Core.Runtime.Hand;
using UnityEditor;
using UnityEngine;

namespace Kinteractions_VR.Interactions.Editors
{
    [CustomEditor(typeof(PoseConstrainter),true)]
    public class PoseConstrainterEditor: Editor
    {
        private PoseConstrainter interactable;
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
                    HandIdentifier.Left => interactable.LeftHandPivot,
                    HandIdentifier.Right => interactable.RightHandPivot,
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
                        CreateHandInPivot(interactable.LeftHandPivot);
                        break;
                    case HandIdentifier.Right:
                         CreateHandInPivot(interactable.RightHandPivot);
                        break;
                }
            }
            get => selectedHand;
        }

        private void DestroyOldHands()
        {
            var rightHandInteractor = interactable.RightHandPivot.GetComponentInChildren<HandPoseController>();
            var leftHandInteractor = interactable.LeftHandPivot.GetComponentInChildren<HandPoseController>();
            if (currentHand) DestroyImmediate(currentHand.attachedGameObject);
            if (rightHandInteractor!=null) DestroyImmediate(rightHandInteractor.attachedGameObject);
            if (leftHandInteractor!=null) DestroyImmediate(leftHandInteractor.attachedGameObject);
        }

        private void CreateHandInPivot(Transform pivot)
        {
            var hand = (selectedHand == HandIdentifier.Left) ? leftHandPrefab.attachedGameObject : rightHandPrefab.attachedGameObject;
            currentHand = Instantiate(hand).GetComponent<HandPoseController>();
            currentHand.attachedGameObject.transform.localScale = Vector3.one;
            currentHand.attachedGameObject.transform.parent = pivot;
            currentHand.attachedGameObject.transform.localPosition = Vector3.zero;
            currentHand.attachedGameObject.transform.localRotation = Quaternion.identity;
            currentHand.Initialize();
        }

        private void OnEnable()
        {
            interactable = (PoseConstrainter)target;
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
            if (interactable.RightHandPivot == null)
            {
                interactable.RightHandPivot= CreateHandPivot("RightHandPivot");
            }

            if (interactable.LeftHandPivot==null)
            {
                interactable.LeftHandPivot = CreateHandPivot("LeftHandPivot");
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
            if(propertyName=="")return;
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