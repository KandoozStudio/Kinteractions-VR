using Kandooz.KVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [System.Serializable]
    public struct HandConstrains
    {
        [MinMax(0, 1, MaxLimit = 1, MinLimit = 0)] public Vector2 indexFingerLimits;
        [MinMax(0, 1, MaxLimit = 1, MinLimit = 0)] public Vector2 MiddleFingerLimits;
        [MinMax(0, 1, MaxLimit = 1, MinLimit = 0)] public Vector2 RingFingerLimits;
        [MinMax(0, 1, MaxLimit = 1, MinLimit = 0)] public Vector2 PinkyFingerLimits;
        [MinMax(0, 1, MaxLimit = 1, MinLimit = 0)] public Vector2 thumbFingerLimits;

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
                        value = Mathf.Clamp(constraints.MiddleFingerLimits.x, constraints.MiddleFingerLimits.y, value);
                        break;
                    case FingerName.Ring:
                        value = Mathf.Clamp(constraints.RingFingerLimits.x, constraints.RingFingerLimits.y, value);
                        break;
                    case FingerName.Pinky:
                        value = Mathf.Clamp(constraints.PinkyFingerLimits.x, constraints.PinkyFingerLimits.y, value);
                        break;
                }
                controller[i] = value;
            }
        }

    }

}