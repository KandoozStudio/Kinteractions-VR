using System;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kandooz.Kuest
{
    [RequireComponent(typeof(StepEventListener))]
    public class TriggerStep : MonoBehaviour
    {
        [SerializeField]private string objectTag; 

        private bool started;
        private StepEventListener listener;

        private void Awake()
        {
            listener = GetComponent<StepEventListener>();
            listener.OnStarted.Do(_ => started = true).Subscribe().AddTo(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!started) return;
            if (string.IsNullOrEmpty(objectTag) || other.CompareTag(objectTag))
            {
                listener.step.OnActionCompleted();
            }
        }
    }
}