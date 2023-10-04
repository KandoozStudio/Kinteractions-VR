using System;
using Kandooz.InteractionSystem.Core;
using Kandooz.InteractionSystem.Interactions;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
//https://navitas.zoom.us/my/ahmad.bakr
namespace Kandooz.Kuest
{
    [RequireComponent(typeof(StepEvenListener))]
    public class ButtonStep : MonoBehaviour
    {
        [SerializeField] private Config config; 
        [SerializeField] private HandIdentifier hand;
        [SerializeField] private XRButton button;
        private StepEvenListener listener;
        private bool started;

        private void Awake()
        {
            if (config == null)
            {
                config = FindAnyObjectByType<CameraRig>().Config;
            }
            listener = GetComponent<StepEvenListener>();
            listener.OnStarted.Do((_) => started = true).Subscribe().AddTo(this);
            listener.OnFinished.Do((_) => started = false).Subscribe().AddTo(this);
            config.InputManager[hand].TriggerObservable
                .Where(_ => started)
                .Where(_ => button == XRButton.Trigger)
                .Where(state => state == ButtonState.Down)
                .Do(_=>listener.OnActionCompleted())
                .Subscribe().AddTo(this);
            config.InputManager[hand].GripObservable
                .Where(_ => started)
                .Where(_ => button == XRButton.Grip)
                .Where(state => state == ButtonState.Down)
                .Do(_=>listener.OnActionCompleted())
                .Subscribe().AddTo(this);
        }

        private void Update()
        {
            if (!started) return;
  
        }
    }
}