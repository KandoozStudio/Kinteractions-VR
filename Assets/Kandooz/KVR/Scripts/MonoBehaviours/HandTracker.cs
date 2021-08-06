using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
#if UNITY_2018 || UNITY_2017
using UnityEngine.XR;
using UnityEngine.SpatialTracking;
#endif

namespace Kandooz.KVR
{
#if UNITY_2018 || UNITY_2017

#else

#endif
    public class HandTracker : MonoBehaviour
    {
        public HandType type;
#if UNITY_2018 || UNITY_2017
        private TrackedPoseDriver pose;
        void Start()
        {
            pose = GetComponent<TrackedPoseDriver>();
            if (!pose)
            {
                pose = gameObject.AddComponent<TrackedPoseDriver>();
            }
            switch (type)
            {
                case HandType.right:
                    pose.SetPoseSource(TrackedPoseDriver.DeviceType.GenericXRController, TrackedPoseDriver.TrackedPose.RightPose);
                    break;
                case HandType.left:
                    pose.SetPoseSource(TrackedPoseDriver.DeviceType.GenericXRController, TrackedPoseDriver.TrackedPose.LeftPose);
                    break;
            }
            
        }
#else
        void Update()
        {
            List<InputDevice> devices= new List<InputDevice>();;
            var device=InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            List<InputFeatureUsage> usages= new List<InputFeatureUsage>();
            device.TryGetFeatureUsages(usages);
            foreach (var usage in usages)
            {
                Debug.Log(usage.type+":"+usage.name);
            }
            switch (type)
            {
                case HandType.left:
                this.transform.localPosition=(InputTracking.GetLocalPosition(XRNode.LeftHand));
                this.transform.localRotation=(InputTracking.GetLocalRotation(XRNode.LeftHand));

                break;
                case HandType.right:
                this.transform.localPosition=(InputTracking.GetLocalPosition(XRNode.RightHand));
                this.transform.localRotation=(InputTracking.GetLocalRotation(XRNode.RightHand));

                break;
            } 
        }
#endif
    }
}
