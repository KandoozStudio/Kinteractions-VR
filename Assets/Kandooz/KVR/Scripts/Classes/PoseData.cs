using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Kandooz.KVR
{
    public struct PoseData
    {
        public AnimationClip open;
        public AnimationClip closed;
        public enum PoseType
        {
            Static,Tweenable
        }
    }
}
