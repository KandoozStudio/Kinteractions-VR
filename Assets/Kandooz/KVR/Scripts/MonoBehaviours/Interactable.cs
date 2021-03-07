using Kandooz.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [System.Serializable]
    public struct SerializedTransform
    {
        public Vector3 position;
        public Vector3 rotation;
    }
    public enum InteractionButton
    {
        None = 0,
        Trigger = 1,
        Grip = 2
    }
    public enum InteractableState
    {
        None = 0,
        Hovered = 1,
        InteractedWith = 2
    }
    public class Interactable : MonoBehaviour
    {
        public HandData handData;
        [SerializeField] private InteractionButton interactionButton;
        [SerializeField] private AbstractVRInputManager inputManager;

        [Header("Events")]
        [HideInInspector] public HandEvent onHandHoverStart;
        [HideInInspector] public HandEvent onHandHoverEnd;
        [HideInInspector] public HandEvent onInteractionStart;
        [HideInInspector] public HandEvent onInteractionEnd;

        [Header("Custom Poses")]
        [SerializeField] [HideInInspector] private bool constraintHandOnHover = false;
        [SerializeField] [HideInInspector] private bool constraintHandOnInteraction = true;

        [HideInInspector] [SerializeField] private HandConstrains hoverHandConstraints = HandConstrains.Pointing;


        [Header("Right Hand properties")]
        [HideInInspector] [SerializeField] private HandConstrains rightHandLimits;
        [HideInInspector] [SerializeField] public SerializedTransform rightHandPivot;

        [Header("Left Hand properties")]
        [HideInInspector] [SerializeField] private HandConstrains leftHandLimits;
        [HideInInspector] [SerializeField] public SerializedTransform leftHandPivot;

        private Hand currentHand;
        [ReadOnly] private InteractableState state;
        protected InteractableState State
        {
            get => state; set
            {
                switch (value)
                {
                    case InteractableState.None:
                        if (currentHand)
                        {
                            currentHand.SetDefaultConstraints();
                        }
                        if (state == InteractableState.Hovered)
                        {
                            onHandHoverEnd.Invoke(currentHand);
                        }
                        else if (state == InteractableState.InteractedWith)
                        {
                            onInteractionEnd.Invoke(currentHand);
                            currentHand.StopInteracting();

                        }
                        currentHand = null;
                        break;
                    case InteractableState.Hovered:
                        if (state != InteractableState.Hovered)
                        {
                            if (constraintHandOnHover)
                            {
                                SetConstraints(hoverHandConstraints);
                            }
                        }
                        break;
                    case InteractableState.InteractedWith:
                        if (state != InteractableState.InteractedWith)
                        {
                            onInteractionStart.Invoke(currentHand);
                            currentHand.StartInteracting();
                            if (constraintHandOnInteraction)
                            {
                                var constraint = (currentHand.hand == HandType.left) ? leftHandLimits : rightHandLimits;
                                SetConstraints(constraint);
                            }
                        }
                        break;
                    default:
                        break;
                }
                state = value;
            }
        }
        public HandConstrains RightHandLimits { get => rightHandLimits; }
        public HandConstrains LeftHandLimits { get => leftHandLimits; }

        public void OnHandHoverStart(Hand hand)
        {
            if (State != InteractableState.InteractedWith)
            {
                this.currentHand = hand;
                State = InteractableState.Hovered;
            }
        }
        public void OnHandHoverEnd(Hand hand)
        {
            State = InteractableState.None;
            onHandHoverEnd.Invoke(hand);
        }
        private void SetConstraints(HandConstrains constraint)
        {
            currentHand.SetHandConstraints(constraint);
        }
        private void Update()
        {
            if (currentHand && interactionButton != InteractionButton.None)
            {
                FingerName finger = (interactionButton == InteractionButton.Trigger) ? FingerName.Trigger : FingerName.Grip;
                bool buttonPressed = inputManager.GetFinger(currentHand.hand, finger);
                switch (State)
                {
                    case InteractableState.Hovered:
                        if (buttonPressed)
                        {
                            State = InteractableState.InteractedWith;
                            currentHand.StartInteracting();
                        }
                        break;
                    case InteractableState.InteractedWith:
                        if (!buttonPressed)
                        {
                            State = InteractableState.None;
                            currentHand.StopInteracting();
                        }
                        break;
                    default:
                        break;
                }
            }

        }
    }
}