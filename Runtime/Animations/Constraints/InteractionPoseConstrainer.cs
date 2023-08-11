using System;
using Kandooz.InteractionSystem.Core;
using Kinteractions_VR.Runtime.Animations.Constraints;
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
        public HandPoseConstraints LeftPoseConstraints => leftConstraints.poseConstraints;
        public HandPoseConstraints RightPoseConstraints => leftConstraints.poseConstraints;
        

        public void OnEnable()
        {
            interactable = GetComponent<InteractableBase>();
            interactable.OnSelected += Constrain;
            interactable.OnSelected += Unconstrain;
        }

        private void Unconstrain(InteractorBase arg0)
        {
            
        }

        private void Constrain(InteractorBase interactor)
        {
            
        }
    }
}