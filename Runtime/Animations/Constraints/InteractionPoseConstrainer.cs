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
        public PoseConstrains LeftPoseConstrains => leftConstraints.poseConstrains;
        public PoseConstrains RightPoseConstrains => rightConstraints.poseConstrains;
        

        public void OnEnable()
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