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

        public float Weight => weight;

        public void SetWeight(float value)
        {
            if (fader == null)
            {
                fader = new CrossFadingFloat();
                fader.onChange += (v) => { this.weight = v; };
            }

            fader.Value = value;
        }
    }
}