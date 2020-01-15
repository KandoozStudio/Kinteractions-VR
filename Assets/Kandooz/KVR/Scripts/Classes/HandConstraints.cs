using Kandooz.KVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [System.Serializable]
    public struct FingerConstraints
    {
        public bool locked;
        [Range(0,1)]
        public float x;
        public float y;
        public static FingerConstraints Free { get
            {
                var finger = new FingerConstraints();
                finger.x = 0;
                finger.y = 1;
                finger.locked = false;
                return finger;
            } }
    }
    [System.Serializable]
    public struct HandConstrains
    {
        public FingerConstraints indexFingerLimits;
        public FingerConstraints middleFingerLimits;
        public FingerConstraints ringFingerLimits;
        public FingerConstraints pinkyFingerLimits;
        public FingerConstraints thumbFingerLimits;
        public static HandConstrains Free
        {
            get
            {
                var hand = new HandConstrains();
                hand.indexFingerLimits = FingerConstraints.Free;
                hand.middleFingerLimits = FingerConstraints.Free;
                hand.ringFingerLimits = FingerConstraints.Free;
                hand.pinkyFingerLimits = FingerConstraints.Free;
                hand.thumbFingerLimits = FingerConstraints.Free;
                return hand;
            }
        }
    }
}