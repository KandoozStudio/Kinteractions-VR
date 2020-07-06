using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kandooz.KVR
{
    public class VariableTweener : MonoBehaviour
    {
        public float tweenScale = 1f;
        private static List<ITweenable> values;

        private void OnEnable()
        {
            values = new List<ITweenable>();
        }
        public void AddCrossFadingFloat( TweenableFloat value)
        {
            values.Add(value);
        }
        void Update()
        {
            for (int i = values.Count-1; i >=0 ; i--)
            {
                if (values[i].Tween(Time.deltaTime*tweenScale))
                {
                    values.RemoveAt(i);
                }
            }
        }
    }

}