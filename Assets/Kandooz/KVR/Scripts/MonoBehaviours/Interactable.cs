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
    public enum GrabbingButton
    {
        Trigger ,
        Grip
    }
    public enum InteractableState
    {
        None,
        Hovered,
        Grapped
    }
    public class Interactable : MonoBehaviour
    {
        public HandData handData;
        public GrabbingButton interactionButton;

        [Header("Events")]
        [HideInInspector] public HandEvent onHandHoverStart;
        [HideInInspector] public HandEvent onHandHoverEnd;
        [HideInInspector] public HandEvent onInteractionStart;
        [HideInInspector] public HandEvent onInteractionEnd;
        [Header("Right Hand properties")]
        [HideInInspector] public HandConstrains rightHandLimits;
        [HideInInspector] public SerializedTransform rightHandPivot;

        [Header("Left Hand properties")]
        [HideInInspector] public HandConstrains leftHandLimits;
        [HideInInspector] public SerializedTransform leftHandPivot;

        public bool InteractedWith { get; set; }
        public InteractableState state { get; set; }
        public void OnHandHoverStart(Hand hand)
        {
            onHandHoverStart.Invoke(hand);
        }
        public void OnHandHoverEnd(Hand hand)
        {
            onHandHoverEnd.Invoke(hand);
        }
        public bool OnInteractionStart(Hand hand)
        {
            if (!InteractedWith)
            {
                onInteractionStart.Invoke(hand);
                InteractedWith = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool OnInteractionEnd(Hand hand)
        {
            InteractedWith = false;
            onInteractionEnd.Invoke(hand);
            return true;
        }

    }
}