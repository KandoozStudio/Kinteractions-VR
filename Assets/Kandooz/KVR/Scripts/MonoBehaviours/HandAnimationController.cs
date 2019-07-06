using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace Kandooz.KVR
{

    public class HandAnimationController : MonoBehaviour
    {
        #region private variables
        [SerializeField] private HandData handData;
        [HideInInspector] [SerializeField] private Finger[] fingers;
        [HideInInspector] [SerializeField] private PlayableGraph graph;
        #endregion
        public float this[FingerName index]
        {
            get { return fingers[(int)index].Weight;}
            set { fingers[(int)index].Weight = value; }
        }

        public float this[int index]
        {
            get { return fingers[index].Weight; }
            set { fingers[index].Weight = value; }
        }

        void Start()
        {
            graph = PlayableGraph.Create();
            fingers = new Finger[5];
            var fingerMixer = AnimationLayerMixerPlayable.Create(graph, fingers.Length);
            for (uint i = 0; i < fingers.Length; i++)
            {
                fingers[i] = new Finger(graph, handData.closed, handData.opened,handData[(int)i]);
                fingerMixer.SetLayerAdditive(i, false);
                fingerMixer.SetLayerMaskFromAvatarMask(i, handData[(int)i]);
                graph.Connect(fingers[i].Mixer, 0, fingerMixer, (int)i);
                fingerMixer.SetInputWeight((int)i, 1);
            }
            var handMixer = AnimationMixerPlayable.Create(graph, 2);
            graph.Connect(fingerMixer, 0, handMixer, 0);
            handMixer.SetInputWeight(0, 1);
            if (handData.poses.Count>0)
            {
                var posesMixer = AnimationMixerPlayable.Create(graph, handData.poses.Count);
                for (int i = 0; i < handData.poses.Count; i++)
                {
                    var pose = AnimationClipPlayable.Create(graph, handData.poses[i]);
                    graph.Connect(pose, 0, posesMixer, i);
                    posesMixer.SetInputWeight(i, 1 );
                    
                }
                graph.Connect(posesMixer, 0, handMixer, 1);
                handMixer.SetInputWeight(1, 0);
            }
            var playableOutput = AnimationPlayableOutput.Create(graph, "Hand Controller", GetComponent<Animator>());
            playableOutput.SetSourcePlayable(handMixer);  
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            graph.Play();
        }
    }
}