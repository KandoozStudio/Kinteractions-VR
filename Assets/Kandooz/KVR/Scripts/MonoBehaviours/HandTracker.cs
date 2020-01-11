using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
#if UNITY_2018 || UNITY_2017
using UnityEngine.XR;
using UnityEngine.SpatialTracking;
#endif
public enum HandType
{
    right, left
}

namespace Kandooz.KVR
{
#if UNITY_2018 || UNITY_2017

#else

#endif
    public class HandTracker : MonoBehaviour
    {
        public HandType hand;
        InputDevice device;
        private void Start()
        {
            switch (hand)
            {
                case HandType.right:
                    device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
                    break;
                case HandType.left:
                    device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
                    break;
                default:
                    break;
            }
        }
        void Update()
        {
            Vector3 position;
            Quaternion rotation;
            device.TryGetFeatureValue(CommonUsages.devicePosition, out position);
            device.TryGetFeatureValue(CommonUsages.deviceRotation, out rotation);
            
            this.transform.localPosition = position;
            this.transform.localRotation= rotation;
        }
    }
}
