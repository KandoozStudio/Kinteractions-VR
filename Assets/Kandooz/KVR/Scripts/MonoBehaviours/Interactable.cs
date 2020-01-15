using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    public class Interactable : MonoBehaviour
    {

        public HandData hand;
        [HideInInspector] public HandConstrains rightHandLimits;
        [HideInInspector] public HandConstrains leftHandLimits;

        [Header("Events")]
        public HandEvent onHandHoverStart;
        public HandEvent onHandHoverEnd;
        public HandEvent onHandGrab;
        public HandEvent onHandRelease;

        private ConstrainedHandControllerStrategy left, right;
        private void OnEnable()
        {
            left = new ConstrainedHandControllerStrategy();
            left.constraints = leftHandLimits;
            right = new ConstrainedHandControllerStrategy();
            right.constraints = rightHandLimits;
        }
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