using Kandooz.Interactions;
using Kandooz.InteractionSystem.Core;
using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{
    public class TriggerInteractor : InteractorBase
    {
        [ReadOnly][SerializeField] private Collider currentCollider;

        private void OnTriggerEnter(Collider other)
        {
            if (ShouldChangeInteractable(other, out var interactable)) return;
            currentInteractable = interactable;
            currentCollider = other;
            OnHoverStart();
        }

        private bool ShouldChangeInteractable(Collider other, out InteractableBase interactable )
        {
            interactable = null;
            if (currentCollider == other) return true;
            if (currentInteractable is { CurrentState: InteractionState.Selected }) return true;

            interactable = other.GetComponentInParent<InteractableBase>();
            return interactable == null;
        }

        private void OnTriggerExit(Collider other)
        {
            
            if( currentInteractable!=null  && currentInteractable.CurrentState== InteractionState.Selected) return;
            if (currentCollider == other)
            {
                currentCollider = null;
                OnHoverEnd();
            }
        }
    }
}