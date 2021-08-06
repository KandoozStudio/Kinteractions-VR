using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Kandooz.KVR
{
    [RequireComponent(typeof(VariableTweener))]
    class HandPoseController : MonoBehaviour
    {
        #region private variables
        [SerializeField] private HandData handData;
        [SerializeField] private float[] fingers = new float[5];
        [SerializeField] public PlayableGraph graph;
        [SerializeField] private int pose;
        [SerializeField] private List<IPose> poses;

        private VariableTweener variableTweener;
        private AnimationMixerPlayable handMixer;
        private Animator animator;

        #endregion
        public float this[FingerName index]
        {
            get { return this[(int)index]; }
            set { this[(int)index] = value; }
        }
        public float this[int index]
        {
            get { return fingers[index]; }
            set
            {
                fingers[index] = value;
                poses[pose][index] = value;
            }
        }
        public int Pose
        {
            set
            {
                {
                    handMixer.SetInputWeight(pose, 0);
                    handMixer.SetInputWeight(value, 1);

                    pose = value;
                    for (int finger = 0; finger < fingers.Length; finger++)
                    {
                        poses[value][finger] = fingers[finger];
                    }
                }
            }
        }

        public void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            if (!handData)
            {
                Debug.LogError("please select a hand data object");
                return;
            }
            GetDependencies();
            InitializeGraph();
        }
        private void GetDependencies()
        {
            variableTweener = GetComponent<VariableTweener>();
            animator = GetComponentInChildren<Animator>();
            if (!animator)
            {
                Debug.LogError("Please add animator to the object or it's children");
            }
        }
        private void InitializeGraph()
        {
            CreateGraphAndSetItsoutputs();
            InitializePoses();
            graph.Play();
        }

        private void CreateGraphAndSetItsoutputs()
        {
            graph = PlayableGraph.Create();
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            handMixer = AnimationMixerPlayable.Create(graph, handData.poses.Count + 1);
            var playableOutput = AnimationPlayableOutput.Create(graph, "Hand mixer", animator);
            playableOutput.SetSourcePlayable(handMixer);
        }

        private void InitializePoses()
        {
            poses = new List<IPose>(handData.poses.Count + 1);
            var pose = CreateAndConnectPose(0, handData.defaultPose);   
            poses.Add(pose);
            for (int i = 0; i < handData.poses.Count; i++)
            {
                pose = CreateAndConnectPose(i + 1, handData.poses[i]);
                poses.Add(pose);
            }
        }

        private IPose CreateAndConnectPose(int poseID, PoseData data)
        {
            if (data.type == PoseData.PoseType.Tweenable)
            {
                var pose = new TweenablePose(graph, data, handData, poseID, variableTweener);
                graph.Connect(pose.PoseMixer, 0, handMixer, poseID);
                pose.PoseMixer.SetInputWeight(0, 1);
                return pose;
            }
            else
            {
                var pose = new StaticPose(graph, data, variableTweener);
                graph.Connect(pose.Mixer, 0, handMixer, poseID);
                return pose;
            }
        }


        public void Update()
        {
            graph.Evaluate();
            for (int i = 0; i < fingers.Length; i++)
            {
                this[i] = fingers[i];
            }
            Pose = pose;

        }
    }
}
