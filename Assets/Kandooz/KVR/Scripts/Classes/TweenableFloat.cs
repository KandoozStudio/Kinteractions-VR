using System;
using UnityEngine;

namespace Kandooz.KVR
{
    public class TweenableFloat:ITweenable
    {
        public event Action<float> onChange;
        private float start;
        private float target;
        private float value;
        private readonly float rate;
        private float t;
        private VariableTweener lerper;
        public float Value
        {
            get
            {
                return value;
            }

            set
            {
#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying)
#endif
                {

                    t = 0;
                    start = this.value;
                    target = value;
                    lerper.AddCrossFadingFloat( this);
                }
#if UNITY_EDITOR
                else
                {
                    this.value = value;
                    onChange(value);
                }
#endif
            }
        }
        public TweenableFloat(VariableTweener lerper,float rate = 2f, float value = 0)
        {
            start = target = this.value = value;
            this.rate = rate;
            t = 1;
            onChange = null;
            this.lerper = lerper;
        }
        public bool Tween(float elapsedTime)
        {
            t +=  rate* elapsedTime;

            this.value = Mathf.Lerp(start, target, t);
            onChange(value);
            return t >= 1;
        }

    }

}