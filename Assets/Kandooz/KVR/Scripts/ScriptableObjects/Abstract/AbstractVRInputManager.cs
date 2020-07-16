using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    public abstract class AbstractVRInputManager :ScriptableObject
    {
        public abstract float GetFingerValue(HandType hand, FingerName finger);
        public abstract bool GetFingerDown(HandType handType, FingerName finger);
        public abstract bool GetFinger(HandType handType, FingerName finger);
        public abstract bool GetFIngerUp(HandType handType, FingerName finger);

    }

}

