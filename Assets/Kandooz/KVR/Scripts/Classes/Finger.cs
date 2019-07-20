using Kandooz.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace Kandooz.KVR
{
    public enum FingerName
    {
        Thumb = 0,
        Index = 1,
        Middle = 2,
        Ring = 3,
        Pinky = 4,
        
    }
    [System.Serializable]
    public class Finger
    {
        AnimationLayerMixerPlayable mixer;
         [Range(0, 1)] [SerializeField] private float weight;
        private float lastweight = 0;
        private CrossFadingFloat crossFadingWeight;
        public float Weight
        {
            set
            {
                if (Mathf.Abs(value - lastweight) > .001f)
                {
                    value = Mathf.Clamp01(value);
                    weight = value;
                    lastweight = value;
                    crossFadingWeight.Value = value;
                }
            }
            get
            {
                return weight;
            }
        }
        public Finger(PlayableGraph graph,AnimationClip closed, AnimationClip opened, AvatarMask mask)
        {
            mixer = AnimationLayerMixerPlayable.Create(graph, 2);
            var openPlayable = AnimationClipPlayable.Create(graph, opened);
            graph.Connect(openPlayable, 0, mixer, 0);
            var closedPlayable = AnimationClipPlayable.Create(graph, closed);
            graph.Connect(closedPlayable, 0, mixer, 1);
            mixer.SetLayerAdditive(0,false);
            mixer.SetLayerMaskFromAvatarMask(0, mask);
            mixer.SetInputWeight(0, 1);
            mixer.SetInputWeight(1, 0);
            crossFadingWeight = new CrossFadingFloat();
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