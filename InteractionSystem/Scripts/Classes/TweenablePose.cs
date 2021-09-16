using System;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Kandooz.KVR
{
    public class TweenablePose : IPose
    {
        private AnimationLayerMixerPlayable poseMixer;
        private Finger[] fingers;
        private HandData handData;
        private int index;
        public float this[int indexer] { set  { 
                fingers[indexer].Weight = value; 
                
            } }

        public AnimationLayerMixerPlayable PoseMixer { get => poseMixer; }
        public string Name { get ; set; }

        public TweenablePose(PlayableGraph graph, PoseData poseData, HandData data, int index, VariableTweener tweener)
        {
            this.index = index;
            this.handData = data;
            fingers = new Finger[5];
            poseMixer = AnimationLayerMixerPlayable.Create(graph, fingers.Length);
            
            for (uint i = 0; i < fingers.Length; i++)
            {
                CreateFingerLayer(i);
                CreateAndConnectFinger(graph, poseData, tweener, i);
            }

        }

        private void CreateFingerLayer(uint i)
        {
            poseMixer.SetLayerAdditive(i, false);
            poseMixer.SetLayerMaskFromAvatarMask(i, handData[(int)i]);
            poseMixer.SetInputWeight((int)i, 1);
        }

        private void CreateAndConnectFinger(PlayableGraph graph, PoseData poseData, VariableTweener tweener, uint i)
        {
            fingers[i] = new Finger(graph, poseData.closed, poseData.open, handData[(int)i], tweener);
            graph.Connect(fingers[i].Mixer, 0, poseMixer, (int)i);
        }

    }

}
