using System;
using UnityEngine;

namespace Kandooz.KVR
{
    public class TweenableVector : ITweenable
    {
        public event Action<Vector3> onChange;
        public event Action onFinished;
        private Vector3 start;
        private Vector3 target;
        private Vector3 value;
        private float rate;
        private float t;
        private VariableTweener lerper;
        public Vector3 Value
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

        public float Rate { set => rate = value; }

        public TweenableVector(VariableTweener lerper,Vector3 value,  float rate = 2f)
        {
            this.start = this.target = this.value = value;
            this.rate = rate;
            this.t = 1;
            this.lerper = lerper;
        }
        public bool Tween(float elapsedTime)
        {
            t += rate * elapsedTime;
            this.value = Vector3.Lerp(start, target, t);
            onChange(value);

            if (t >= 1)
            {
                onFinished();
                return true;
            }
            return false;
        }

    }

}