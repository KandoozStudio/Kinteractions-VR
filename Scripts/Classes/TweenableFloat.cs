﻿using System;
using UnityEngine;

namespace Kandooz.KVR
{
    public class TweenableFloat : ITweenable
    {
        public event Action<float> onChange;
        private float start;
        private float target;
        private float value;
        private float rate;
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
                    lerper.AddTweenable(this);
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

        public float Rate {  set => rate = value; }

        public TweenableFloat(VariableTweener lerper,Action<float> onChange=null, float rate = 1f, float value = 0)
        {
            start = target = this.value = value;
            this.rate = rate;
            this.t = 1;
            this.onChange = onChange;
            this.lerper = lerper;
        }
        public bool Tween(float elapsedTime)
        {
            t += rate * elapsedTime;

            this.value = Mathf.Lerp(start, target, t);
            onChange(value);
            return t >= 1;
        }

    }

}