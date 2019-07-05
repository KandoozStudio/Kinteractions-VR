using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace Kandooz.KVR
{
    public enum FingerName
    {
        Thumb=0,
        Index=1,
        Middle=2,
        Ring=3,
        Pinky=4
    }
    public class HandAnimationController : MonoBehaviour
    {
        [SerializeField]private HandData handData;
        [HideInInspector] public PlayableGraph graph;
        [HideInInspector] [SerializeField]private Finger[] fingers;
        #region private variables
        AnimationLayerMixerPlayable indexPlayable;
        #endregion
        public float this[FingerName index]
        {
            get { return fingers[(int)index].Weight;}
            set { fingers[(int)index].Weight = value; }
        }
        

        void Start()
        {
            graph = PlayableGraph.Create();
            fingers = new Finger[5];
            var handMixer = AnimationLayerMixerPlayable.Create(graph, fingers.Length);
            for (uint i = 0; i < fingers.Length; i++)
            {
                fingers[i] = new Finger(graph, handData.closed, handData.opened);
                handMixer.SetLayerAdditive(i, false);
                handMixer.SetLayerMaskFromAvatarMask(i, handData[i]);
                graph.Connect(fingers[i].Mixer, 0, handMixer, (int)i);
                handMixer.SetInputWeight((int)i, 1);
            }
            var playableOutput = AnimationPlayableOutput.Create(graph, "Hand Controller", GetComponent<Animator>());
            playableOutput.SetSourcePlayable(handMixer);
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            graph.Play();
        }
    }
}