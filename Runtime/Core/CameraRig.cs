using Kandooz.InteractionSystem.Animations;
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
        [SerializeField] private Config config;
        [SerializeField] private float playerHeight;
        public IPoseable LeftHandPrefab => poseData.LeftPosablePrefab;
        public IPoseable RightHandPrefab => poseData.rightPoseablePrefab;

        public void InitializePhysicsBasedHands()
        {
            var offsetObject = new GameObject("Offset").transform;
            //var leftHand = new GameObject("LeftHand").AddComponent<>();
            //leftHand.
        }
    }
}