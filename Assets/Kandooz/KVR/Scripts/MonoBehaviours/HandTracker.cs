using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
#if UNITY_2018 || UNITY_2017
        public HandType type;
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

#endif
    }
}
