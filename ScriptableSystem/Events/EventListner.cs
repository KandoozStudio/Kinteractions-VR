using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kandooz.ScriptableSystem
{

    [System.Serializable]
    public class EventListner<T> : MonoBehaviour
    {
       [SerializeField] private List<ScriptableEventAndUnityEventPair<T>> events;
        private void OnEnable()
        {
            foreach (var @event in events)
            {
                @event.scriptableEvent?.Subscribe(@event.OnInvoke);
            }
        }
        private void OnDisable()
        {
            foreach (var @event in events)
            {
                @event.scriptableEvent?.Unsubscribe(@event.OnInvoke);
            }
        }
    }
    [System.Serializable]
    public class EventListner : MonoBehaviour
    {
        [HideInInspector] [SerializeField] private List<ScriptableEventAndUnityEventPair> events;
        private void OnEnable()
        {
            foreach (var @event in events)
            {
                @event.scriptableEvent?.Subscribe(@event.OnInvoke);
            }
        }
        private void OnDisable()
        {
            foreach (var @event in events)
            {
                @event.scriptableEvent?.Unsubscribe(@event.OnInvoke);
            }

        }
    }

}

