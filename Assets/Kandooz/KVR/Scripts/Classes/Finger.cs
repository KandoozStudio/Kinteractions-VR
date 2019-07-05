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
        public float Weight
        {
            set
            {
                Debug.Log("hmmmm");
                value = Mathf.Clamp01(value);
                mixer.SetInputWeight(0, 1 - value);
                mixer.SetInputWeight(1, value);
            }
            get
            {
                return weight;
            }
        }
        public Finger(PlayableGraph graph,AnimationClip closed, AnimationClip opened, AvatarMask mask,uint id=0)
        {
            mixer = AnimationLayerMixerPlayable.Create(graph, 2);
            mixer.SetLayerAdditive(id, false);
            mixer.SetLayerMaskFromAvatarMask(id, mask);
            var statePlayable = AnimationClipPlayable.Create(graph, opened);
            graph.Connect(statePlayable, 0, Mixer, 0);
            statePlayable = AnimationClipPlayable.Create(graph, closed);
            graph.Connect(statePlayable, 0, Mixer, 1);
            
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