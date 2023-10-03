using System;
using Kandooz.InteractionSystem.Interactions;
using UniRx;
using UnityEngine;

namespace Kandooz.Kuest
{
    [RequireComponent(typeof(StepEventListener))]
    public class InteractionStep : MonoBehaviour
    {
        [SerializeField] private InteractableBase interactableObject;
        private StepEventListener listener;

        private void Awake()
        {
            listener = GetComponent<StepEventListener>();
            listener.OnStarted.Do((_) =>interactableObject.OnSelected+=OnInteractionStarted).Subscribe().AddTo(this);
            listener.OnFinished.Do((_) => interactableObject.OnSelected-=OnInteractionStarted).Subscribe().AddTo(this);
        }


        private void OnInteractionStarted(InteractorBase interactor)
        {
                listener.step.OnActionCompleted();
        }
    }
}