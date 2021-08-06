using Kandooz.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Kandooz.KVR
{
    class HandPoseController
    {
        #region private variables
        [HideInInspector] [SerializeField] private HandData handData;
        [HideInInspector] [SerializeField] private Finger[] fingers;
        [HideInInspector] [SerializeField] public PlayableGraph graph;
        [HideInInspector] [SerializeField] private bool staticPose;

        [HideInInspector] [SerializeField] private List<IPose> poses;
        private CrossFadingFloat staticPoseCrossFader;
        private AnimationMixerPlayable handMixer;
        private bool initialized;
        AnimationMixerPlayable poseMixer;
        [HideInInspector] [SerializeField] private int pose;
        #endregion

    }
}
