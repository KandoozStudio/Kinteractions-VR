using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Kandooz.InteractionSystem.Core
{
    [RequireComponent(typeof(VariableTweener))]
    public class HandPoseController : MonoBehaviour
    {
        #region private variables
        [HideInInspector] [SerializeField] private HandData handData;
        [Range(0, 1)]
        [HideInInspector] [SerializeField] private float[] fingers = new float[5];
        [HideInInspector] [SerializeField] private int pose;

        private List<IPose> poses;
        private VariableTweener variableTweener;
        private AnimationMixerPlayable handMixer;
        private Animator animator;
        private PlayableGraph graph;

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
            get
            {
                return pose;
            }
        }
        public HandData HandData { get => handData; set => handData = value; }
        public PlayableGraph Graph { get => graph; }
        public List<IPose> Poses { get => poses; }
        public void Start()
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
            CreateAndConnectPose(0, handData.defaultPose);

            for (int i = 0; i < handData.poses.Count; i++)
            {
                CreateAndConnectPose(i + 1, handData.poses[i]);
            }
        }
        private void CreateAndConnectPose(int poseID, PoseData data)
        {
            IPose pose;
            if (data.type == PoseData.PoseType.Tweenable)
            {
                pose = CreateTweenablePose(poseID, data);
            }
            else
            {
                pose = CreateStaticPose(poseID, data);
            }
            pose.Name = data.Name;
            poses.Add(pose);
        }
        private IPose CreateStaticPose(int poseID, PoseData data)
        {
            var pose = new StaticPose(graph, data, variableTweener);
            pose.Name = data.Name;
            graph.Connect(pose.Mixer, 0, handMixer, poseID);
            return pose;
        }
        private IPose CreateTweenablePose(int poseID, PoseData data)
        {
            var pose = new TweenablePose(graph, data, handData, poseID, variableTweener);
            graph.Connect(pose.PoseMixer, 0, handMixer, poseID);
            pose.PoseMixer.SetInputWeight(0, 1);
            return pose;
        }
        public void Update()
        {
            if (!graph.IsValid())
            {
                InitializeGraph();
            }
            graph.Evaluate();
            for (int i = 0; i < fingers.Length; i++)
            {
                this[i] = fingers[i];
            }
            Pose = pose;
        }
    }
}
