


using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Kandooz.KVR
{
    class TweenablePose : IPose
    {
        Finger[] fingers;
        AnimationLayerMixerPlayable mixer;
        AnimationClip opened;
        AnimationClip closed;
        VariableTweener tweener;
        PlayableGraph graph;
        HandData handData;
        public float this[FingerName index] { set => this[(int)index] = value; }
        public float this[int index] { set => fingers[index].Weight = value; }

        public TweenablePose(PlayableGraph graph,VariableTweener tweener,HandData handData)
        {
            this.graph = graph;
            this.tweener = tweener;
            this.handData = handData;
            AnimationLayerMixerPlayable handMixer = GetHandMixer();
            handMixer = AnimationMixerPlayable.Create(graph, 2);
            graph.Connect(handMixer, 0, handMixer, 0);
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
                        var pose = new Pose(lerper);
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
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            graph.Play();
        }

        private AnimationLayerMixerPlayable GetHandMixer()
        {
            fingers = new Finger[5];
            var handMixer = AnimationLayerMixerPlayable.Create(graph, fingers.Length);
            for (uint i = 0; i < fingers.Length; i++)
            {
                fingers[i] = new Finger(graph, closed, opened, handData[(int)i], tweener);
                handMixer.SetLayerAdditive(i, false);
                handMixer.SetLayerMaskFromAvatarMask(i, handData[(int)i]);
                graph.Connect(fingers[i].Mixer, 0, handMixer, (int)i);
                handMixer.SetInputWeight((int)i, 1);
            }

            return handMixer;
        }
        public float Weight { 
            set => throw new System.NotImplementedException(); 
        }
    }
}
