using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kandooz.Common {
    public class CrossFadingFloat
    {
        public event Action<float> onChange;
        private float start;
        private float target;
        private float value;
        private float rate;
        private float t;
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
                    CrossFadingFloatLerper.Instance.AddCrossFadingFloat( this);
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
        public CrossFadingFloat(float rate = 5f, float value = 0)
        {
            start = target = this.value = value;
            this.rate = rate;
            t = 1;
            onChange = null;
        }
        public bool Step()
        {
            t +=  rate*Time.deltaTime;

            this.value = Mathf.Lerp(start, target, t);
            onChange(value);
            return t >= 1;
        }

    }
    public class CrossFadingFloatLerper : MonoBehaviour
    {
        private static List<CrossFadingFloat> values;
        private static CrossFadingFloatLerper instance;

        public static CrossFadingFloatLerper Instance
        {
            get
            {
                if (!instance)
                {
                    instance = new GameObject("CrossFadingFloatLerper").AddComponent<CrossFadingFloatLerper>();
                    values = new List<CrossFadingFloat>();
                    DontDestroyOnLoad(instance);
                }
                return instance;
            }
        }
        
        public void AddCrossFadingFloat( CrossFadingFloat value)
        {
            values.Add(value);
        }
        void Update()
        {
            for (int i = values.Count-1; i >=0 ; i--)
            {
                if (values[i].Step())
                {
                    values.RemoveAt(i);
                }
            }
        }
    }

}