using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    public class AxisbasedXRInput : AbstractVRInputManager
    {
        public override float GetAxis(HandSource hand, Axis axisName)
        {
            throw new System.NotImplementedException();
        }

        public override bool GetButton(HandSource hand, Button button)
        {
            throw new System.NotImplementedException();
        }

        public override bool GetButtonDown(HandSource hand, Button button)
        {
            throw new System.NotImplementedException();
        }

        public override bool GetButtonUp(HandSource hand, Button button)
        {
            throw new System.NotImplementedException();
        }

        public override float GetFinger(HandSource hand, FingerName finger)
        {
            throw new System.NotImplementedException();
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }

    internal class HandAxis
    {
        public string[] fingerAxisNames = new string[5];
        public string triggerAxisName;
        public string GripAxisName;
        public string IndexButtonName;
        public string GripButtonName;
        
    }
}
