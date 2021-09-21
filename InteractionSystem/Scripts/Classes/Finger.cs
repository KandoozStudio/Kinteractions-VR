using Kandooz.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace Kandooz.KVR
{

    [System.Serializable]
    public class Finger
    {
        AnimationLayerMixerPlayable mixer;
        [Range(0, 1)] [SerializeField] private float weight;
        private TweenableFloat crossFadingWeight;

        public float Weight
        {
            set
            {
                if (Mathf.Abs(value - weight) > .01f)
                {
                    weight = value;
                    crossFadingWeight.Value = value;
                }
            }
            get
            {
                return weight;
            }
        }
        public Finger(PlayableGraph graph, AnimationClip closed, AnimationClip opened, AvatarMask mask, VariableTweener lerper)
        {
            mixer = AnimationLayerMixerPlayable.Create(graph, 2);
            var openPlayable = AnimationClipPlayable.Create(graph, opened);
            graph.Connect(openPlayable, 0, mixer, 0);
            var closedPlayable = AnimationClipPlayable.Create(graph, closed);
            graph.Connect(closedPlayable, 0, mixer, 1);
            mixer.SetLayerAdditive(0, false);
            mixer.SetLayerMaskFromAvatarMask(0, mask);
            mixer.SetInputWeight(0, 1);
            mixer.SetInputWeight(1, 0);
            crossFadingWeight = new TweenableFloat(lerper);
            crossFadingWeight.onChange += (value) =>
            {
                mixer.SetInputWeight(0, 1 - value);
                mixer.SetInputWeight(1, value);
            };
        }

        public AnimationLayerMixerPlayable Mixer
        {
            get
            {
                return mixer;
            }
        }
    }
}