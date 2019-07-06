using System.Collections;
using System.Collections.Generic;
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
    }
    public class HandAnimationController : MonoBehaviour
    {
        #region private variables
        [SerializeField] private HandData handData;
        [HideInInspector] [SerializeField] private Finger[] fingers;
        [HideInInspector] [SerializeField] private PlayableGraph graph;
        [HideInInspector] [SerializeField] private bool staticPose;
        private List<Pose> poses;
        private float fingerPoseRatio=0;
        private float fingerPoseTimer=1;
        private readonly float crossFadeSpeed=0.2f;
        private AnimationMixerPlayable handMixer;
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
                    fingerPoseTimer = 0;
                }
                staticPose = value;
            }
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
        void Start()
        {
            graph = PlayableGraph.Create("Hand Animation Controller graph");
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
            handMixer = AnimationMixerPlayable.Create(graph, 2);
            graph.Connect(fingerMixer, 0, handMixer, 0);
            handMixer.SetInputWeight(0, 1);
            if (handData.poses.Count>0)
            {
                var posesMixer = AnimationMixerPlayable.Create(graph, handData.poses.Count);
                poses = new List<Pose>();
                for (int i = 0; i < handData.poses.Count; i++)
                {
                    var poseClip = handData.poses[i];
                    if (poseClip)
                    {
                        var pose = new Pose();
                        pose.playable=AnimationClipPlayable.Create(graph, handData.poses[i]);
                        pose.clip = handData.poses[i];
                        poses.Add(pose);
                        graph.Connect(pose.playable, 0, posesMixer, i);
                        posesMixer.SetInputWeight(i, 1);
                    }
                }
                if (poses.Count > 0)
                {
                    graph.Connect(posesMixer, 0, handMixer, 1);
                    handMixer.SetInputWeight(1, 0);
                }
            }
            var playableOutput = AnimationPlayableOutput.Create(graph, "Hand Controller", GetComponent<Animator>());
            playableOutput.SetSourcePlayable(handMixer);
            GraphVisualizerClient.Show(graph);
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            graph.Play();
        }
        void OnDisable()
        {
            graph.Destroy();
        }
        void Update()
        {
            fingerPoseTimer += crossFadeSpeed;
            fingerPoseRatio = (staticPose) ? Mathf.Lerp(0, 1, fingerPoseTimer) : Mathf.Lerp(1, 0, fingerPoseTimer);
            handMixer.SetInputWeight(0, 1-fingerPoseRatio);
            handMixer.SetInputWeight(1, fingerPoseRatio);
        }
    }
}