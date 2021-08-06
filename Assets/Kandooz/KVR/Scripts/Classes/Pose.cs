using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Animations;

namespace Kandooz.KVR
{

    [System.Serializable]
    public class Pose
    {
        public AnimationClipPlayable playable;
        public AnimationClip clip;
        private float weight;
        private TweenableFloat fader;
        private VariableTweener lerper;

        public float Weight => weight;
        public Pose(VariableTweener lerper)
        {
            this.lerper = lerper;
        }
        public void SetWeight(float value)
        {
            if (fader == null)
            {
                fader = new TweenableFloat(lerper);
                fader.onChange += (v) => { this.weight = v; };
            }

            fader.Value = value;
        }
    }
}