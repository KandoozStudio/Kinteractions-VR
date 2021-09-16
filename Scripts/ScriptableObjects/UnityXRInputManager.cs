
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Kandooz.KVR
{
    [CreateAssetMenu(menuName ="Kandooz/KVR/InputManager")]
    public class UnityXRInputManager : AbstractVRInputManager
    {
        InputDevice handDevice;
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();


        public override float GetFinger(HandSource hand, FingerName finger)
        {
            GetHandDevices(hand);
            return GetFingerValue(handDevice, finger);
        }

        private float GetFingerValue(InputDevice device, FingerName finger)
        {
            float value = 0;
            switch (finger)
            {
                case FingerName.Thumb:

                    if (ThumbPressed(device))
                    {
                        value = 1;
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

        private static bool ThumbPressed(InputDevice device)
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

        private void GetHandDevices(HandSource source)
        {
            switch (source)
            {
                case HandSource.Right:
                    UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, devices);
                    break;
                case HandSource.Left:
                    UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, devices);
                    break;
                default:
                    break;
            }
            if (devices.Count > 0)
            {
                handDevice = devices[0];
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
