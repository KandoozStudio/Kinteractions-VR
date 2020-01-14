using Kandooz.KVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [System.Serializable]
    
    public struct MinMaxFloat
    {
        [MinMax(0, 1, MaxLimit = 1, MinLimit = 0)]
        public Vector2 limits;
    }
    [System.Serializable]
    public struct HandConstrains
    {
        public MinMaxFloat indexFingerLimits;
        public MinMaxFloat MiddleFingerLimits;
        public MinMaxFloat RingFingerLimits;
        public MinMaxFloat PinkyFingerLimits;
        public MinMaxFloat thumbFingerLimits;

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
                        value = Mathf.Clamp(constraints.thumbFingerLimits.limits.x, constraints.thumbFingerLimits.limits.y, value);
                        break;
                    case FingerName.Index:
                        value = Mathf.Clamp(constraints.indexFingerLimits.limits.x, constraints.indexFingerLimits.limits.y, value);
                        break;
                    case FingerName.Middle:
                        value = Mathf.Clamp(constraints.MiddleFingerLimits.limits.x, constraints.MiddleFingerLimits.limits.y, value);
                        break;
                    case FingerName.Ring:
                        value = Mathf.Clamp(constraints.RingFingerLimits.limits.x, constraints.RingFingerLimits.limits.y, value);
                        break;
                    case FingerName.Pinky:
                        value = Mathf.Clamp(constraints.PinkyFingerLimits.limits.x, constraints.PinkyFingerLimits.limits.y, value);
                        break;
                }
                controller[i] = value;
            }
        }

    }

}