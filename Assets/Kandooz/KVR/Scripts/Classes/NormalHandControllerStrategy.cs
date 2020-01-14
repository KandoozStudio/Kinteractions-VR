using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    public class NormalHandControllerStrategy : IHandControlerStrategy
    {
        public void UpdateHand(HandAnimationController controller, VRInputManager inputManager, HandType hand)
        {
            for(int i = 0; i < 5; i++)
            {
                controller[i] = inputManager.GetFingerValue(hand, (FingerName)i);
            }
        }
    }
}