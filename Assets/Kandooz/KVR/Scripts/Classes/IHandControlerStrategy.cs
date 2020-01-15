﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    public interface IHandControlerStrategy
    {
          void UpdateHand(HandAnimationController controller, VRInputManager inputManager, HandType hand);
    }

}