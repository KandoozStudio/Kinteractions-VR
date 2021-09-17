
using UnityEngine;

namespace Kandooz.KVR
{
    public abstract class AbstractVRInputManager : ScriptableObject
    {
        public abstract float GetFinger(HandSource hand,FingerName finger);
        public abstract bool GetButton(HandSource hand, Button button);
        public abstract float GetAxis(HandSource hand, Axis axisName);
        public abstract bool GetButtonDown(HandSource hand, Button button);
        public abstract bool GetButtonUp(HandSource hand, Button button);
    }
}
