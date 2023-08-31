
using Kandooz.InteractionSystem.Animations.Constraints;
using Kandooz.InteractionSystem.Core;
using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{
    [RequireComponent(typeof(InteractableBase))]
    public class InteractionPoseConstrainer : MonoBehaviour,IPoseConstrainer
    {
        [HideInInspector, SerializeField] private HandConstraints leftConstraints;
        [HideInInspector, SerializeField] private HandConstraints rightConstraints;
        [HideInInspector, SerializeField] private Transform pivotParent;
        private InteractableBase interactable;
        public Transform LeftHandTransform
        {
            set => leftConstraints.relativeTransform = value;
            get => leftConstraints.relativeTransform;
        }
        public Transform RightHandTransform
        {
            set => rightConstraints.relativeTransform = value;
            get => rightConstraints.relativeTransform;
        }

        public Transform PivotParent => pivotParent;
        public PoseConstrains LeftPoseConstrains => leftConstraints.poseConstrains;
        public PoseConstrains RightPoseConstrains => rightConstraints.poseConstrains;

        public void UpdatePivotParent()
        {
            if (pivotParent == null)
            {
                pivotParent = new GameObject("Interaction Pivots").transform;
            }
            pivotParent.parent = null;
            pivotParent.localScale = Vector3.one;
            pivotParent.parent = this.transform;
        }

        private void OnEnable()
        {
            interactable = GetComponent<InteractableBase>();
            interactable.OnSelected += Constrain;
            interactable.OnDeselected += Unconstrain;
        }

        private void OnDisable()
        {
            interactable.OnSelected += Constrain;
            interactable.OnDeselected += Unconstrain;
        }

        private void Unconstrain(InteractorBase interactor)
        {
            interactor.Hand.Unconstrain(this);
        }

        private void Constrain(InteractorBase interactor)
        {
            interactor.Hand.Constrain(this);
        }
    }
}