using System;
using UnityEngine;

namespace Kandooz.KVR
{
    [Serializable]
    public struct PoseData
    {
        public AnimationClip open;
        public AnimationClip closed;
        public PoseType type;
        public enum PoseType
        {
            Static,Tweenable
        }
    }
}
