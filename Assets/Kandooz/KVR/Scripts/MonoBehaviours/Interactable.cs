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
    [System.Flags]
    public enum GrabbingButton
    {
        Index =1,
        Grip=2
    }
    public class Interactable : MonoBehaviour
    {
        public HandData hand;
        public GrabbingButton interactionButton;

        [Header("Events")]
        public HandEvent onHandHoverStart;
        public HandEvent onHandHoverEnd;
        public HandEvent onHandGrab;
        public HandEvent onHandRelease;
        [Header("Right Hand properties")]
        [HideInInspector] public HandConstrains rightHandLimits;
        [Header("Left Hand properties")]
        [HideInInspector] public HandConstrains leftHandLimits;
        [HideInInspector] public SerializedTransform leftHandPivot;
        [HideInInspector] public SerializedTransform rightHandPivot;

        

        public void OnHandHoverStart(HandInputManager hand)
        {
            onHandHoverStart.Invoke(hand);
        }
        public void OnHandHoverEnd(HandInputManager hand)
        {
            onHandHoverEnd.Invoke(hand);
        }

        public void OnGrabStart(HandInputManager hand)
        {
            onHandGrab.Invoke(hand);
        }
        public void OnRelease(HandInputManager hand)
        {
            onHandRelease.Invoke(hand);
        }
    }
}