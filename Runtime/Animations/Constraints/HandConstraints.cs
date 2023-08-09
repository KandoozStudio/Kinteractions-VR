
using UnityEngine;
using UnityEngine.Serialization;

namespace Kandooz.InteractionSystem.Core
{
    [System.Serializable]
    public struct HandConstraints
    {
        //public HandPoseController prefab;
        public HandPoseConstraints poseConstraints;
        public Transform relativeTransform;

        // public void ConstraintHand(HandInputMapper mapper)
        // {
        //     mapper.Constraints = constraints;
        // }
        // public void UnConstraintHand(HandInputMapper mapper)
        // {
        //     mapper.Constraints = HandConstraints.Free;
        // }
    }

}

