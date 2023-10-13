using Kandooz.InteractionSystem.Animations;
using Kandooz.InteractionSystem.Animations.Constraints;
using Kandooz.InteractionSystem.Core;
using UnityEditor;
using UnityEngine;

namespace Kandooz.Interactions.Editors
{
    public abstract class AbstractPoseConstraintEditor : Editor
    {
        protected IPoseConstrainter interactable;
        private float t = 0;
        private Transform Pivot
        {
            get
            {
                return selectedHand switch
                {
                    HandIdentifier.Left => LeftHandTransform,
                    HandIdentifier.Right => RightHandTransform,
                    _ => null
                };
            }
        }
        protected HandPoseController currentHand;
        protected HandPoseController leftHandPrefab, rightHandPrefab;
        protected HandIdentifier selectedHand;
        private PoseConstrains LeftPoseConstrains => interactable.LeftPoseConstrains;
        private PoseConstrains RightPoseConstrains => interactable.LeftPoseConstrains;
        private Transform RightHandTransform
        {
            get => interactable.RightHandTransform;
            set => interactable.RightHandTransform = value;
        }
        private Transform LeftHandTransform
        {
            get => interactable.LeftHandTransform;
            set => interactable.LeftHandTransform = value;
        }
        private Transform PivotParent => interactable.PivotParent;

        protected abstract HandIdentifier SelectedHand { set; }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (interactable.HasChanged)
                interactable.UpdatePivots();
            HandleHandSelection();
            ShowPoseInspector();
            SetPose();
        }

        protected abstract void ShowPoseInspector();
        private void HandleHandSelection()
        {
            EditorGUILayout.BeginHorizontal();
            var style = new GUIStyle(GUI.skin.button);
            var rightHandClicked = GUILayout.Toggle(selectedHand == HandIdentifier.Right, "RightHand Constraints", style) ^
                                   selectedHand == HandIdentifier.Right;
            var leftHandClicked = GUILayout.Toggle(selectedHand == HandIdentifier.Left, "LeftHand Constraints", style) ^ selectedHand == HandIdentifier.Left;

            if (rightHandClicked)
            {
                SelectHand(selectedHand == HandIdentifier.Right ? HandIdentifier.None : HandIdentifier.Right);
                interactable.UpdatePivots();
            }

            if (leftHandClicked)
            {
                interactable.UpdatePivots();
                SelectHand(selectedHand == HandIdentifier.Left ? HandIdentifier.None : HandIdentifier.Left);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void InitializeHandPivot()
        {
            interactable.UpdatePivots();
            if (RightHandTransform == null)
            {
                RightHandTransform = CreateHandPivot("RightHandPivot");
            }

            if (LeftHandTransform == null)
            {
                LeftHandTransform = CreateHandPivot("LeftHandPivot");
            }
        }

        protected HandPoseController CreateHandInPivot(Transform pivot, HandPoseController hand)
        {
            var initializedHand = Instantiate(hand).GetComponent<HandPoseController>();
            var handObject = initializedHand.gameObject;
            handObject.transform.localScale = Vector3.one;
            handObject.transform.parent = pivot;
            handObject.transform.localPosition = Vector3.zero;
            handObject.transform.localRotation = Quaternion.identity;
            initializedHand.Initialize();
            return initializedHand;
        }

        protected virtual void OnEnable()
        {
            interactable = (IPoseConstrainter)target;
            interactable.Initialize();
            InitializeHandPivot();
            selectedHand = HandIdentifier.None;
            var cameraRig = FindAnyObjectByType<CameraRig>();
            if (!cameraRig) return;
            leftHandPrefab = cameraRig.LeftHandPrefab;
            rightHandPrefab = cameraRig.RightHandPrefab;
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

        private void OnSceneGUI()
        {
            SetPose();
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

        private void SetPose()
        {
            this.t += 0.005f;
            var t = Mathf.PingPong(this.t, 1);
            if (selectedHand == HandIdentifier.None) return;
            var handConstraints = selectedHand == HandIdentifier.Left ? LeftPoseConstrains : RightPoseConstrains;
            for (int i = 0; i < 5; i++)
            {
                currentHand[i] = handConstraints[i].GetConstrainedValue(t);
            }

            currentHand.UpdateGraphVariables();
        }

        private Transform CreateHandPivot(string pivotName)
        {
            var handTransform = new GameObject(pivotName).transform;
            handTransform.parent = PivotParent;
            handTransform.localPosition = Vector3.zero;
            handTransform.localRotation = Quaternion.identity;
            return handTransform;
        }

        protected virtual void OnDisable()
        {
            SelectedHand = HandIdentifier.None;
            Tools.hidden = false;
            DeselectHands();
        }

        protected abstract void DeselectHands();
    }
}