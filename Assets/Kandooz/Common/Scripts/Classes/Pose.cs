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
        private CrossFadingFloat fader;
        private CrossFadingFloatLerper lerper;

        public float Weight => weight;
        public Pose(CrossFadingFloatLerper lerper)
        {
            this.lerper = lerper;
        }
        public void SetWeight(float value)
        {
            if (fader == null)
            {
                fader = new CrossFadingFloat(lerper);
                fader.onChange += (v) => { this.weight = v; };
            }

            fader.Value = value;
        }
    }
}