using Kandooz.KVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [System.Serializable]
    
    public struct MinMaxFloat
    {
        /// <summary>
        /// MinMaxFloat helps you convert a value that is normaly between 0 and 1 be between min and max
        /// </summary>
        [MinMax(0, 1, MaxLimit = 1, MinLimit = 0)]
        public Vector2 limits;


    }
    [System.Serializable]
    public class ConstrainedHandControllerStrategy : IHandControlerStrategy
    {
        public MinMaxFloat indexFingerLimits;
        public MinMaxFloat MiddleFingerLimits;
        public MinMaxFloat RingFingerLimits;
        public MinMaxFloat PinkyFingerLimits;
        public MinMaxFloat thumbFingerLimits;

        public void UpdateHand(HandAnimationController controller, VRInputManager inputManager, HandType hand)
        {
            for (int i = 0; i < 5; i++)
            {
                var finger = (FingerName)i;
                var value = inputManager.GetFingerValue(hand, finger);
                switch (finger)
                {
                    case FingerName.Thumb:
                        value = Mathf.Clamp(thumbFingerLimits.limits.x, thumbFingerLimits.limits.y, value);
                        break;
                    case FingerName.Index:
                        value = Mathf.Clamp(indexFingerLimits.limits.x, indexFingerLimits.limits.y, value);
                        break;
                    case FingerName.Middle:
                        value = Mathf.Clamp(MiddleFingerLimits.limits.x, MiddleFingerLimits.limits.y, value);
                        break;
                    case FingerName.Ring:
                        value = Mathf.Clamp(RingFingerLimits.limits.x, RingFingerLimits.limits.y, value);
                        break;
                    case FingerName.Pinky:
                        value = Mathf.Clamp(PinkyFingerLimits.limits.x, PinkyFingerLimits.limits.y, value);
                        break;
                }
                controller[i] = value;
            }
        }

    }

}