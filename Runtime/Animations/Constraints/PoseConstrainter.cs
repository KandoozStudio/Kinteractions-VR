using Kandooz.InteractionSystem.Core;
using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{
    public class PoseConstrainter :MonoBehaviour
    {
        [HideInInspector, SerializeField] private HandConstraints leftConstraints;
        [HideInInspector, SerializeField] private HandConstraints rightConstraints;
        public Transform LeftHandPivot
        {
            set => leftConstraints.relativeTransform = value;
            get => leftConstraints.relativeTransform;
        }
        public Transform RightHandPivot
        {
            set => rightConstraints.relativeTransform = value;
            get => rightConstraints.relativeTransform;
        }

        public HandPoseConstraints LeftPoseConstraints => leftConstraints.poseConstraints;
        public HandPoseConstraints RightPoseConstraints => leftConstraints.poseConstraints;

    }
}