
using Kandooz.Common;
using UnityEngine;

namespace Kandooz.KVR
{
    /// <summary>
    /// Responsible for managing the interaction with the object ( notifing all subscripers of changes )
    /// </summary>
    public class Interactable : MonoBehaviour
    {
        [SerializeField] Button interActionButton;
        [Header(header: "Events")]
        public InteractorEvent onHoverStart;
        public InteractorEvent onHoverEnd;
        public InteractorEvent onHover;
        public InteractorEvent onInteractionStart;
        public InteractorEvent onInteractionEnd;
        [Header(header: "Debug")]
        [ReadOnly] [SerializeField] Interactor currentInteractor;
        [ReadOnly] [SerializeField] Interactor currentHoverer;
        [ReadOnly] [SerializeField] bool interacted;

        public void OnHoverStart(Interactor interactor)
        {
            if (currentHoverer != null)
            {
                OnHoverEnd(currentHoverer);
            }
            onHoverStart.Invoke(interactor);
        }
        public void OnHoverEnd(Interactor interactor)
        {
            currentHoverer = null;
            onHoverEnd.Invoke(interactor);
        }
        public void OnHover(Interactor interactor)
        {
            onHover.Invoke(interactor);
            if (interacted) 
                return;
            var interactionButtonPressed = interactor.Mapper.InputManager.GetButtonDown(interactor.Mapper.Hand, interActionButton);
            if (interactionButtonPressed)
            {
                interactor.StartInteraction();
                OnInterationStart(interactor);
            }
        }
        public void OnInterationStart(Interactor interactor)
        {
            interacted = true;
            currentInteractor = interactor;
            onInteractionStart.Invoke(interactor);
            interactor.StartInteraction();
        }
        public void OnInterationEnd(Interactor interactor)
        {
            interacted = false;
            onInteractionEnd.Invoke(interactor);
            interactor.EndInteraction(this);

            currentInteractor = null;
        }
        void Update()
        {
            if (currentInteractor)
            {
                var InteractionUp = currentInteractor.Mapper.InputManager.GetButtonUp(currentInteractor.Mapper.Hand, interActionButton);
                if (InteractionUp)
                {
                    OnInterationEnd(currentInteractor);

                }
            }
        }
    }
}
