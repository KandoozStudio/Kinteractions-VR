using Kandooz.InteractionSystem.Interactions;
using UniRx;
using UnityEngine;

namespace Kandooz.Kuest
{
    public enum InteractionType
    {
        Selection=0,
        Deselection=1,
        Activation=2,
        HoverStart=3,
        HoverEnd=4,
    }
    [RequireComponent(typeof(StepEvenListener))]
    public class InteractionAction : MonoBehaviour
    {
        [SerializeField] private InteractableBase interactableObject;
        [SerializeField] private InteractionType interactionType;

        private StepEvenListener listener;
        
        private void Awake()
        {
            listener = GetComponent<StepEvenListener>();
            listener.OnStarted.Do((_) => Subscribe()).Subscribe().AddTo(this);
            listener.OnFinished.Do((_) => Unsubscribe()).Subscribe().AddTo(this);
        }

        void Subscribe()
        {
            switch (interactionType)
            {
                case InteractionType.Selection:
                    interactableObject.OnSelected += OnInteractionStarted;
                    break;
                case InteractionType.Deselection:
                    interactableObject.OnDeselected += OnInteractionStarted;
                    break;
                case InteractionType.Activation:
                    interactableObject.OnActivated += OnInteractionStarted;
                    break;
                case InteractionType.HoverStart:
                    interactableObject.OnHoverStarted += OnInteractionStarted;
                    break;
                case InteractionType.HoverEnd:
                    interactableObject.OnHoverEnded += OnInteractionStarted;
                    break;
            }

        }
        void Unsubscribe()
        {
            switch (interactionType)
            {
                case InteractionType.Selection:
                    interactableObject.OnSelected -= OnInteractionStarted;
                    break;
                case InteractionType.Deselection:
                    interactableObject.OnDeselected -= OnInteractionStarted;
                    break;
                case InteractionType.Activation:
                    interactableObject.OnActivated -= OnInteractionStarted;
                    break;
                case InteractionType.HoverStart:
                    interactableObject.OnHoverStarted -= OnInteractionStarted;
                    break;
                case InteractionType.HoverEnd:
                    interactableObject.OnHoverEnded -= OnInteractionStarted;
                    break;
            }

        }

        private void OnInteractionStarted(InteractorBase interactor)
        {
                listener.OnActionCompleted();
        }
    }
}