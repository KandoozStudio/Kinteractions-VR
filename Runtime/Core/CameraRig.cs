using Kinteractions_VR.Core.Runtime.Hand;
using UnityEngine;

namespace Kandooz.InteractionSystem.Core
{
    public enum InteractionSystemType
    {
        TransformBased,
        PhysicsBased
    }
    public class CameraRig : MonoBehaviour,IPoseablePrefabsHolder
    {
        [SerializeField] private GameObject leftHand;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private InteractionSystemType interactionSystemType;
        public IPoseable leftHandPoseable => leftHand.GetComponentInChildren<IPoseable>();
        public IPoseable rightHandPoseable => rightHand.GetComponentInChildren<IPoseable>();


        public IPoseable LeftHandPrefab => leftHandPoseable.LeftHandPrefab;
        public IPoseable RightHandPrefab => rightHandPoseable.RightHandPrefab;
    }
}