using Kandooz.Common;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Kandooz.KVR
{
    [System.Serializable]
    public class Pose
    {
        public AnimationClipPlayable playable;
        public AnimationClip clip;
        private float weight;
        private CrossFadingFloat fader;

        public float Weight
        {
            get
            {
                return weight;
            }
        }

        public void SetWeight(float value)
        {
            if (fader == null)
            {
                var that = this;
                fader = new CrossFadingFloat();
                fader.onChange += (v) =>
                {
                    that.weight = v;
                };
            }
            fader.Value = value;
        }
    }
    public class HandAnimationController : MonoBehaviour
    {
        #region private variables
        [HideInInspector] [SerializeField] private HandData handData;
        [HideInInspector] [SerializeField] private Finger[] fingers;
        [HideInInspector] [SerializeField] public PlayableGraph graph;
        [HideInInspector] [SerializeField] private bool staticPose;
        [HideInInspector] [SerializeField] private List<Pose> poses;
        private CrossFadingFloat staticPoseCrossFader;
        private AnimationMixerPlayable handMixer;
        private bool initialized;
        AnimationMixerPlayable poseMixer;
        [HideInInspector] [SerializeField] private int pose;
        #endregion

        public bool StaticPose
        {
            get
            {
                return staticPose;
            }
            set
            {
                if (value != staticPose)
                {
                    if (staticPoseCrossFader == null)
                    {
                        staticPoseCrossFader = new CrossFadingFloat();
                        staticPoseCrossFader.onChange += (v) => {
                            handMixer.SetInputWeight(0, 1-v);
                            handMixer.SetInputWeight(1, v);
                        };
                    }
                    staticPoseCrossFader.Value = (value) ? 1 : 0;
                    
                }
                staticPose = value;
            }
        }

        public int Pose
        {
            get
            {
                return pose;
            }
            set
            {
                if (pose != value)
                {
                    poses[pose].SetWeight(0);
                    poses[value].SetWeight(1);
                }
                pose = value;
            }
        }

        public HandData HandData
        {
            get { return handData; }
        }

        public bool Initialized
        {
            get { return initialized; }
        }

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
        public void Init()
        {
            if (handData) {
                graph = PlayableGraph.Create("Hand Animation Controller graph");
                fingers = new Finger[5];
                var fingerMixer = AnimationLayerMixerPlayable.Create(graph, fingers.Length);
                for (uint i = 0; i < fingers.Length; i++)
                {
                    fingers[i] = new Finger(graph, handData.closed, handData.opened, handData[(int)i]);
                    fingerMixer.SetLayerAdditive(i, false);
                    fingerMixer.SetLayerMaskFromAvatarMask(i, handData[(int)i]);
                    graph.Connect(fingers[i].Mixer, 0, fingerMixer, (int)i);
                    fingerMixer.SetInputWeight((int)i, 1);
                }
                handMixer = AnimationMixerPlayable.Create(graph, 2);
                graph.Connect(fingerMixer, 0, handMixer, 0);
                handMixer.SetInputWeight(0, 1);
                if (handData.poses.Count > 0)
                {
                    poseMixer = AnimationMixerPlayable.Create(graph, handData.poses.Count);
                    poses = new List<Pose>();
                    for (int i = 0; i < handData.poses.Count; i++)
                    {
                        var poseClip = handData.poses[i];
                        if (poseClip)
                        {
                            var pose = new Pose();
                            pose.playable = AnimationClipPlayable.Create(graph, handData.poses[i]);
                            pose.clip = handData.poses[i];
                            poses.Add(pose);
                            graph.Connect(pose.playable, 0, poseMixer, i);
                            poseMixer.SetInputWeight(i, 0);
                        }
                        poseMixer.SetInputWeight(pose, 1);
                    }
                    if (poses.Count > 0)
                    {
                        graph.Connect(poseMixer, 0, handMixer, 1);
                        handMixer.SetInputWeight(1, 0);
                    }
                }
                var playableOutput = AnimationPlayableOutput.Create(graph, "Hand Controller", GetComponentInChildren<Animator>());
                playableOutput.SetSourcePlayable(handMixer);
                initialized = true;
                graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
                graph.Play();

            }
            else
            {
                initialized = false;
            }
        }
        void Start()
        {
            if (!initialized)
            {
                Init();
            }
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            //graph.Play();
        }
        void OnDisable()
        {
            graph.Destroy();
        }
        public void Update()
        {
            if (!EditorApplication.isPlaying)
            {
                //graph.SetTimeUpdateMode(UnityEngine.Playables.DirectorUpdateMode.Manual);
                graph.Evaluate();
            }
            graph.Evaluate();

            for (int i = 0; i < poses.Count; i++)
            {
                try
                {
                    poseMixer.SetInputWeight(i, poses[i].Weight);
                }
                catch
                {
                    poses.RemoveAt(i);
                }
            }
        }
    }
}