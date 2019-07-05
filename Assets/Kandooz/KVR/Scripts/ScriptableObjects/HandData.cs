using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Kandooz.KVR
{
    public enum HandType
    {
        threeFinger = 3,
        fiveFinger = 5
    }
    [CreateAssetMenu(menuName = "Kandooz/KVR/Hand Data")]
    public class HandData : ScriptableObject
    {
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
        public AvatarMask this[uint i]
        {
            get
            {
                AvatarMask mask = thumbAvatarMask;
                switch (i)
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