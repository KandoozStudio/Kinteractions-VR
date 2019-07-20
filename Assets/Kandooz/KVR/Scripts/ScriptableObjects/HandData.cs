using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Kandooz.KVR
{

    [CreateAssetMenu(menuName = "Kandooz/KVR/Hand Data")]
    public class HandData : ScriptableObject
    {
        [Tooltip("The Hand must have HandAnimationController Script")]
        public HandAnimationController leftHandPrefab;
        public HandAnimationController rightHandPrefab;
        [Header("Avatar Masks")]
        [SerializeField] public AvatarMask thumbAvatarMask;
        [SerializeField] public AvatarMask indexAvatarMask;
        [SerializeField] public AvatarMask middleAvatarMask;
        [SerializeField] public AvatarMask ringAvatarMask;
        [SerializeField] public AvatarMask pinkyAvatarMask;

        [Header("Animation clips")]
        public AnimationClip opened;
        public AnimationClip closed;
        [HideInInspector] public List<AnimationClip> poses;
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
}