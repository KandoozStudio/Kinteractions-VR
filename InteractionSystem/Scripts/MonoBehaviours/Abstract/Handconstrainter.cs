using Kandooz.KVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [System.Serializable]
    public struct Handconstrainter
    {
        public HandPoseController prefab;
        public HandConstraints constraints;
        public Transform relativeTransform;

        public void ConstraintHand(HandInputMapper mapper)
        {
            mapper.Constraints = constraints;
        }
        public void UnConstraintHand(HandInputMapper mapper)
        {
            mapper.Constraints = HandConstraints.Free;
        }
    }

}

