using Kandooz.KVR;
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
    //controls the hand pose /
    public class PoseController : MonoBehaviour
    {
        [SerializeField] HandData handData;
        [HideInInspector] [SerializeField] public PlayableGraph graph;
        [HideInInspector] [SerializeField] private List<IPose> poses;
        [HideInInspector] [SerializeField] private int currentPose;
        
        Finger[] fingers = new Finger[5];
        private TweenableFloat staticPoseCrossFader;
        private AnimationMixerPlayable handMixer;
        AnimationMixerPlayable poseMixer;
        private VariableTweener lerper;

        public float this[int index]
        {
            get { return fingers[index].Weight; }
            set { fingers[index].Weight = value; }
        }
        public float this[FingerName index]
        {
            get { return fingers[(int)index].Weight; }
            set { fingers[(int)index].Weight = value; }
        }

        public void Awake()
        {
            if (handData)
            {
                InitializeVariableTweener();
                InitializeGraph();
            }
            else
            {
                Debug.LogError("hand data was not defined please drag the correct data object");
            }
        }

        private void InitializeGraph()
        {
            graph = PlayableGraph.Create("Pose Driver graph");


        }
        private void InitializeVariableTweener()
        {
            lerper = GetComponent<VariableTweener>();
            if (!lerper)
            {
                lerper = this.gameObject.AddComponent<VariableTweener>();
            }
        }
    }
}
