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
    public class Interactable : MonoBehaviour
    {
        public HandData hand;

        [Header("Events")]
        public HandEvent onHandHoverStart;
        public HandEvent onHandHoverEnd;
        public HandEvent onHandGrab;
        public HandEvent onHandRelease;

        [HideInInspector] public HandConstrains rightHandLimits;
        [HideInInspector] public HandConstrains leftHandLimits;
        [HideInInspector] public SerializedTransform leftHandPivot;
        [HideInInspector] public SerializedTransform rightHandPivot;

        public void OnHandHoverStart(HandController hand)
        {
            onHandHoverStart.Invoke(hand);
        }
        public void OnHandHoverEnd(HandController hand)
        {
            onHandHoverEnd.Invoke(hand);
        }

        public void OnGrabStart(HandController hand)
        {
            onHandGrab.Invoke(hand);
        }
        public void OnRelease(HandController hand)
        {
            onHandRelease.Invoke(hand);
        }
    }
}