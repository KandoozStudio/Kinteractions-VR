using Kandooz.KVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [System.Serializable]
    public struct FingerConstraints
    {
        /// <summary>
        /// Defines whether the finger is locked or free
        /// </summary>
        public bool locked;
        [Range(0,1)]
        public float min;
        public float max;
        public FingerConstraints(bool locked,float min,float max)
        {
            this.min = min;
            this.max = max;
            this.locked = locked;
        }


        /// <summary>
        /// Short for new FingerConstraints(false,0,1);
        /// </summary>
        public static FingerConstraints Free { get
            {
                

                return new FingerConstraints(false, 0, 1); ;
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

        /// <summary>
        /// A non constrained Hand
        /// </summary>
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