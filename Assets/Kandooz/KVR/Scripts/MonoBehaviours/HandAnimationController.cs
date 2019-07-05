using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace Kandooz.KVR
{
    public class HandAnimationController : MonoBehaviour
    {
        public HandData handData;
        [HideInInspector] public PlayableGraph graph;
        public Finger[] fingers;
        #region private variables
        AnimationLayerMixerPlayable indexPlayable;
        #endregion

        public float Thumb
        {
            get
            {
                return fingers[0].Weight;
            }
            set
            {
                fingers[0].Weight = value;
            }
        }

        public float Index
        {
            get
            {
                return fingers[1].Weight;
            }
            set
            {
                fingers[1].Weight = value;
            }
        }

        public float Middle
        {
            get
            {
                return fingers[2].Weight;
            }
            set
            {
                fingers[2].Weight = value;
            }
        }

        public float Ring
        {
            get
            {
                return fingers[3].Weight;
            }
            set
            {
                fingers[3].Weight = value;
            }
        }

        public float Pinky
        {
            get
            {
                return fingers[4].Weight;
            }
            set
            {
                fingers[4].Weight = value;
            }
        }

        public float Grip
        {
            get
            {
                return fingers[2].Weight;
            }
            set
            {
                fingers[2].Weight = value;
            }
        }

        void Start()
        {
            graph = PlayableGraph.Create();
            fingers = new Finger[5];

            var handMixer = AnimationLayerMixerPlayable.Create(graph, fingers.Length);
            for (uint i = 0; i < fingers.Length; i++)
            {
                fingers[i] = new Finger(graph, handData.closed, handData.opened, handData[i], 0);
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

        void Update()
        {
            foreach (var finger in fingers)
            {
                finger.Weight = finger.Weight;
            }
        }
    }
}