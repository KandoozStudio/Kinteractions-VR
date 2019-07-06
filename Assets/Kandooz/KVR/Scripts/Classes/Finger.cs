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
        Pinky = 4
    }
    [System.Serializable]
    public class Finger
    {
        AnimationLayerMixerPlayable mixer;
         [Range(0, 1)] [SerializeField] private float weight;
        public float Weight
        {
            set
            {
                value = Mathf.Clamp01(value);
                mixer.SetInputWeight(0, 1 - value);
                mixer.SetInputWeight(1, value);
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
            graph.Connect(openPlayable, 0, Mixer, 0);
            var closedPlayable = AnimationClipPlayable.Create(graph, closed);
            graph.Connect(closedPlayable, 0, Mixer, 1);
            mixer.SetLayerAdditive(0,false);
            mixer.SetLayerMaskFromAvatarMask(0, mask);
            mixer.SetInputWeight(0, 1);
            mixer.SetInputWeight(1, 0);
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