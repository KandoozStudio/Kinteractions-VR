using System;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kandooz.Kuest
{
    [RequireComponent(typeof(StepEventListener))]
    public class ButtonStep : MonoBehaviour
    {
        [SerializeField]private string buttonName;
        private StepEventListener listener;
        private bool started;

        private void Awake()
        {
            listener = GetComponent<StepEventListener>();
            listener.OnStarted.Do((_) => started = true).Subscribe().AddTo(this);
            listener.OnFinished.Do((_) => started = false).Subscribe().AddTo(this);
        }

        private void Update()
        {
            if (!started) return;
            if (Input.GetButton(buttonName))
            {
                listener.step.OnActionCompleted();
            }
        }
    }
}