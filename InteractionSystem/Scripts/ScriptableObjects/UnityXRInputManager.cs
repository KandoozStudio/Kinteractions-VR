
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kandooz.KVR
{
    [CreateAssetMenu(menuName = "Kandooz/KVR/InputManager")]
    public class UnityXRInputManager : AbstractVRInputManager
    {
        public UnityEngine.InputSystem.InputActionAsset asset;
        UnityEngine.XR.InputDevice handDevice;
        Dictionary<Button, UnityEngine.InputSystem.InputAction> rightButtonsDown;
        Dictionary<Button, UnityEngine.InputSystem.InputAction> leftButtonsDown;
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        public override float GetFinger(HandSource hand, FingerName finger)
        {
            GetHandDevices(hand);
            return GetFingerValue(handDevice, finger);
        }
        private float GetFingerValue(UnityEngine.XR.InputDevice device, FingerName finger)
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
        private static bool ThumbPressed(UnityEngine.XR.InputDevice device)
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
            bool value = false;
            GetHandDevices(hand);
            handDevice.TryGetFeatureValue(new UnityEngine.XR.InputFeatureUsage<bool>(button.ToString()), out value);
            return value;
        }
        public override bool GetButtonDown(HandSource hand, Button button)
        {
            var handButtons = hand == HandSource.Left ? leftButtonsDown : rightButtonsDown;
            
            //var value = handButtons.ContainsKey(button) != null ? handButtons[button].ReadValue<bool>() : false;
            return false;
        }
        public override bool GetButtonUp(HandSource hand, Button button)
        {
            GetHandDevices(hand);

            return Input.GetKeyUp(KeyCode.Escape);
        }

        private void OnEnable()
        {
            Awake();
        }
        private void OnDisable()
        {

        }
        private void Awake()
        {
            InitializeButtonsDictionaries();
            var obj = new GameObject();

            for (int i = 0; i < 11; i++)
            {
                var button = (Button)i;
                InitializeButtonValue(button);
                InitializeHandCallbacks(button, "Right",rightButtonsDown);
                InitializeHandCallbacks(button, "Left",leftButtonsDown);
            }
        }
        private void InitializeHandCallbacks(Button button, string hand, Dictionary<Button,InputAction> handAction)

        {
            var buttonName = hand + button.ToString();
            try
            {

                handAction[button] = asset.FindAction(buttonName);
                handAction[button].started += (ctx) =>
                {
                    Debug.Log("yes"+buttonName);
                };
                handAction[button].canceled+= (ctx) =>
                {
                    Debug.Log("no"+buttonName);
                };
            }
            catch (System.Exception e)
            {
                Debug.Log(buttonName);
            }
        }

        private void InitializeButtonValue(Button button, bool value = false)
        {
            //rightButtonsDown[button] = value;
            //leftButtonsDown[button] = value;
        }

        private void InitializeButtonsDictionaries()
        {
            rightButtonsDown ??= new Dictionary<Button, UnityEngine.InputSystem.InputAction>();
            leftButtonsDown ??= new Dictionary<Button, UnityEngine.InputSystem.InputAction>();
        }
    }
}
