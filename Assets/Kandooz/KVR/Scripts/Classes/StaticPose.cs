
using System;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Kandooz.KVR
{
    class StaticPose : IPose
    {
        public float this[FingerName index] { set  { } }
        public float this[int index] { set { } }

        private AnimationClipPlayable playable;

        public AnimationClipPlayable Mixer { get => playable;  }

        public StaticPose(PlayableGraph graph, PoseData poseData, VariableTweener tweener)
        {
            playable = AnimationClipPlayable.Create(graph, poseData.open);
        }

    }
}
