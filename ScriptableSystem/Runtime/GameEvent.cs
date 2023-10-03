using System;
using UniRx;
using UnityEngine;

namespace Kandooz.ScriptableSystem
{
    public class GameEvent<T> : ScriptableObject
    {
        protected Subject<T> onNotify = new();
        public IObservable<T> OnNotify => onNotify;
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return onNotify.Subscribe(observer);
        }
        public void Raise(T data)
        {
            try
            {
                onNotify.OnNext(data);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        protected virtual void Complete()
        {
            try
            {
                onNotify.OnCompleted();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}