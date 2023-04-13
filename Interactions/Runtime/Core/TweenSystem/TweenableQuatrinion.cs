using System;
using UnityEngine;

namespace Kandooz.Interactions.Runtime.Core
{
    public class TweenableQuatrinion : ITweenable
    {
        public event Action<Quaternion> onChange;
        public event Action onFinished;
        private Quaternion start;
        private Quaternion target;
        private Quaternion value;
        private float rate;
        private float t;
        private VariableTweener lerper;
        public Quaternion Value
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

        public TweenableQuatrinion(VariableTweener lerper,Quaternion value,  float rate = 2f)
        {
            this.start = this.target = this.value = value;
            this.rate = rate;
            this.t = 1;
            this.lerper = lerper;
        }
        public bool Tween(float elapsedTime)
        {
            t += rate * elapsedTime;
            this.value = Quaternion.Lerp(start, target, t);
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