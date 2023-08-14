using Kandooz.InteractionSystem.Core;
using Kinteractions_VR.Runtime.Animations.Constraints;
using UnityEngine;

namespace Kinteractions_VR.Core.Runtime.Hand
{
    // TODO: Seperate the prefab Holders from eachother
    public interface IPoseable
    {
        public float this[int index]
        {
            get;
            set;
        }
        public float this[FingerName index]
        {
            get;
            set;
        }
        public int Pose
        {
            set;
        }
        public PoseConstrains  Constrains { set; } 
    }
    
}