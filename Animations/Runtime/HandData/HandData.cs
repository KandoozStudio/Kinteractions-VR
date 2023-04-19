using System.Collections.Generic;
using Kandooz.InteractionSystem.Core;
using UnityEngine;


namespace Kandooz.InteractionSystem.Animations
{

    [CreateAssetMenu(menuName = "Kandooz/KVR/Hand Data")]
    public class HandData : ScriptableObject,IAvatarMaskIndexer
    {
        [Tooltip("The Hand must have HandAnimationController Script attached")]
        //public HandAnimationController leftHandPrefab;
        //public HandAnimationController rightHandPrefab;
        [Header("Avatar Masks")]
        [SerializeField] public AvatarMask thumbAvatarMask;
        [SerializeField] public AvatarMask indexAvatarMask;
        [SerializeField] public AvatarMask middleAvatarMask;
        [SerializeField] public AvatarMask ringAvatarMask;
        [SerializeField] public AvatarMask pinkyAvatarMask;

        [Header("Default pose clips")] [HideInInspector]
        public PoseData defaultPose;

        [Header("Custom Poses")] [HideInInspector]
        public List<PoseData> poses;
        public AvatarMask this[FingerName i]
        {
            get
            {
                AvatarMask mask = thumbAvatarMask;
                switch ((int)i)
                {
                    case 0:
                        mask = thumbAvatarMask;
                        break;
                    case 1:
                        mask = indexAvatarMask;
                        break;
                    case 2:
                        mask = middleAvatarMask;
                        break;
                    case 3:
                        mask = ringAvatarMask;
                        break;
                    case 4:
                        mask = pinkyAvatarMask;
                        break;
                }
                return mask;
            }
        }
        public AvatarMask this[int i]
        {
            get
            {
                AvatarMask mask = thumbAvatarMask;
                switch ((int)i)
                {
                    case 0:
                        mask = thumbAvatarMask;
                        break;
                    case 1:
                        mask = indexAvatarMask;
                        break;
                    case 2:
                        mask = middleAvatarMask;
                        break;
                    case 3:
                        mask = ringAvatarMask;
                        break;
                    case 4:
                        mask = pinkyAvatarMask;
                        break;
                }
                return mask;
            }
        }

    }

    public interface IAvatarMaskIndexer
    {
        public AvatarMask this[int i] { get; }
    }
}