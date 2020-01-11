using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    public class InputDeviceHandController : IHandControlerStrategy
    {
        private HandAnimationController controller;
        private VRInputManager inputManager;
        private HandType hand;
        public void UpdateHand()
        {
            for(int i = 0; i < 5; i++)
            {
                controller[i] = inputManager.GetFingerValue(hand, (FingerName)i);
            }
        }
        public  InputDeviceHandController(HandAnimationController controller, VRInputManager inputManager,HandType hand)
        {
            this.controller = controller;
            this.inputManager = inputManager;
            this.hand = hand;
        }
    }
}