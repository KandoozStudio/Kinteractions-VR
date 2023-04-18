
using UnityEngine;
namespace Kandooz.InteractionSystem.Core
{
    [System.Serializable]
    public struct HandConstraintData
    {
        public HandPoseController prefab;
        public HandConstraints constraints;
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

