using System;
using UniRx;
using UnityEngine;

namespace Kandooz.Kuest
{
    [RequireComponent(typeof(StepEventListener))]
    public class AudioStep : MonoBehaviour
    {
        [SerializeField] private  AudioClip clip;
        private AudioSource source;
        private bool started=false;
        private StepEventListener listener;

        private void OnEnable()
        {
            source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;
            source.clip = clip;
            listener = GetComponent<StepEventListener>();
            listener.OnStarted.Do(_ => source.Play())
                .SelectMany(_ => Observable.EveryUpdate())
                .Where(_ => !source.isPlaying)
                .Do(_ => listener.step.OnActionCompleted())
                .Subscribe()
                .AddTo(this);
            listener.OnFinished.Do(_=>source.Stop()).Subscribe().AddTo(this);
        }

        private void Update()
        {
            if(!started ||source.isPlaying)return;
            listener.step.OnActionCompleted();
            started = false;
        }
    }
}