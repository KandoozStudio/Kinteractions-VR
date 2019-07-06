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
    public class Finger
    {
        AnimationMixerPlayable mixer;
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
        public Finger(PlayableGraph graph,AnimationClip closed, AnimationClip opened)
        {
            mixer = AnimationMixerPlayable.Create(graph, 2);
            var statePlayable = AnimationClipPlayable.Create(graph, opened);
            graph.Connect(statePlayable, 0, Mixer, 0);
            statePlayable = AnimationClipPlayable.Create(graph, closed);
            graph.Connect(statePlayable, 0, Mixer, 1);
            
            mixer.SetInputWeight(0, 1);
            mixer.SetInputWeight(1, 0);
        }

        public AnimationMixerPlayable Mixer
        {
            get
            {
                return mixer;
            }
        }
    }
}