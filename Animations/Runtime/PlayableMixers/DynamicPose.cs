using Kandooz.InteractionSystem.Core;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Kandooz.InteractionSystem.Animations
{
    public class DynamicPose : IPose
    {
        private AnimationLayerMixerPlayable poseMixer;
        private readonly FingerAnimationMixer[] fingers;
        private readonly IAvatarMaskIndexer handData;
        public float this[int indexer] { set => fingers[indexer].Weight = value; }

        public AnimationLayerMixerPlayable PoseMixer => poseMixer;
        public string Name { get ; set; }

        public DynamicPose(PlayableGraph graph, PoseData poseData, IAvatarMaskIndexer data, VariableTweener tweener)
        { 
            this.handData = data;
            fingers = new FingerAnimationMixer[5];
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
            fingers[i] = new FingerAnimationMixer(graph, poseData.ClosedAnimationClip, poseData.OpenAnimationClip, handData[(int)i], tweener);
            graph.Connect(fingers[i].Mixer, 0, poseMixer, (int)i);
        }

    }

}
