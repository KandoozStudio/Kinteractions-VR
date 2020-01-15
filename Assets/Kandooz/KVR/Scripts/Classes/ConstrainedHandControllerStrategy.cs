using Kandooz.KVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [System.Serializable]
    public struct FingerConstraiint
    {
        public bool locked;
        [Range(0,1)]
        public float x;
        public float y;
    }
    [System.Serializable]
    public struct HandConstrains
    {
        public FingerConstraiint indexFingerLimits;
        public FingerConstraiint middleFingerLimits;
        public FingerConstraiint ringFingerLimits;
        public FingerConstraiint pinkyFingerLimits;
        public FingerConstraiint thumbFingerLimits;

    }
    public class ConstrainedHandControllerStrategy : IHandControlerStrategy
    {
        public HandConstrains constraints;
        public void UpdateHand(HandAnimationController controller, VRInputManager inputManager, HandType hand)
        {
            for (int i = 0; i < 5; i++)
            {
                var finger = (FingerName)i;
                var value = inputManager.GetFingerValue(hand, finger);
                switch (finger)
                {
                    case FingerName.Thumb:
                        value = Mathf.Clamp(constraints.thumbFingerLimits.x, constraints.thumbFingerLimits.y, value);
                        break;
                    case FingerName.Index:
                        value = Mathf.Clamp(constraints.indexFingerLimits.x, constraints.indexFingerLimits.y, value);
                        break;
                    case FingerName.Middle:
                        value = Mathf.Clamp(constraints.middleFingerLimits.x, constraints.middleFingerLimits.y, value);
                        break;
                    case FingerName.Ring:
                        value = Mathf.Clamp(constraints.ringFingerLimits.x, constraints.ringFingerLimits.y, value);
                        break;
                    case FingerName.Pinky:
                        value = Mathf.Clamp(constraints.pinkyFingerLimits.x, constraints.pinkyFingerLimits.y, value);
                        break;
                }
                controller[i] = value;
            }
        }

    }

}