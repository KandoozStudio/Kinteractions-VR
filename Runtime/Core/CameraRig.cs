using Kandooz.InteractionSystem.Animations;
using Kinteractions_VR.Core.Runtime.Hand;
using UnityEngine;

namespace Kandooz.InteractionSystem.Core
{
    public enum InteractionSystemType
    {
        TransformBased,
        PhysicsBased
    }
    public class CameraRig : MonoBehaviour
    {
        [SerializeField] private GameObject leftHand;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private InteractionSystemType interactionSystemType;
        [SerializeField] private HandPoseData poseData;
        public IPoseable LeftHandPrefab => poseData.LeftPosablePrefab;
        public IPoseable RightHandPrefab => poseData.rightPoseablePrefab;
    }
}