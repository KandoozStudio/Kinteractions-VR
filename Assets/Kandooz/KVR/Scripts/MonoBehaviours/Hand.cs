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

    public class Hand : MonoBehaviour
    {
        public HandType hand;
        public HandConstrains defaultHandConstraints= HandConstrains.Free;
        private HandConstrains constraints = HandConstrains.Free;
        InputDevice device;

        public HandConstrains Constraints { get => constraints;  }

        public void Start()
        {
            SetDefaultConstraints();
        }
        public void SetHandConstraints(HandConstrains constraints)
        {
            this.constraints = constraints;
        }
        public void SetDefaultConstraints()
        {
            this.constraints = this.defaultHandConstraints;
        }
        
    }
}
