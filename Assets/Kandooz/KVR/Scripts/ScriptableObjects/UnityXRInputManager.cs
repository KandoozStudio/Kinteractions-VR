
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Kandooz.KVR
{
    public class UnityXRInputManager : AbstractVRInputManager
    {
        InputDevice leftHandDevice;
        InputDevice rightHandDevice;

        public override float GetFinger(HandSource hand, FingerName finger)
        {
            GetHandDevices();
            float value=0;
            switch (hand)
            {
                case HandSource.Right:
                    value = GetFingerValue(rightHandDevice,finger);
                    break;
                case HandSource.Left:
                    value = GetFingerValue(leftHandDevice,finger);
                    break;
            }
            return value;
        }

        private float GetFingerValue(InputDevice device,FingerName finger)
        {
            float value=0;
            switch (finger)
            {
                case FingerName.Thumb:

                    if (GripPressed(device))
                    {
                        value =1;
                    }
                    break;
                case FingerName.Index:
                    device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out value);
                    break;
                case FingerName.Middle:
                case FingerName.Ring:
                case FingerName.Pinky:
                    device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out value);
                    break;
            }
            return value;
        }

        private static bool GripPressed(InputDevice device)
        {
            bool gripPressed = false;
            bool temp = false;
            device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out temp);
            gripPressed |= temp;
            device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out temp);
            gripPressed |= temp;
            device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryTouch, out temp);
            gripPressed |= temp;
            return gripPressed;
        }

        private void GetHandDevices()
        {

            var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
            if (leftHandDevices.Count > 0)
            {
                leftHandDevice = leftHandDevices[0];
            }

            var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
            if (rightHandDevices.Count > 0)
            {
                rightHandDevice = leftHandDevices[0];
            }
        }

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




    }
}
