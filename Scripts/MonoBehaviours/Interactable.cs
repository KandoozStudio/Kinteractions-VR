
using Kandooz.Common;
using UnityEngine;

namespace Kandooz.KVR
{
    /// <summary>
    /// Responsible for managing the interaction with the object ( notifing all subscripers of changes )
    /// </summary>
    public class Interactable : MonoBehaviour
    {
        [SerializeField] Button InterActionButton;
        [Header(header: "Events")]
        [SerializeField] InteractorEvent onHoverStart;
        [SerializeField] InteractorEvent onHoverEnd;
        [SerializeField] InteractorEvent onHover;
        [SerializeField] InteractorEvent onInteractionStart;
        [SerializeField] InteractorEvent onInteractionEnd;
        [Header(header: "Debug")]

        [ReadOnly][SerializeField] Interactor currentInteractor;

        IPose pose;
        public IPose Pose { get => pose; set => pose = value; }

        public void OnHoverStart(Interactor interactor)
        {
            onHoverStart.Invoke(interactor);
        }
        public void OnHoverEnd(Interactor interactor)
        {
            onHoverEnd.Invoke(interactor);
        }
        public void OnHover(Interactor interactor)
        {
            onHover.Invoke(interactor);
            var interactionButtonPressed = interactor.Mapper.InputManager.GetButtonDown(interactor.Mapper.Hand, InterActionButton);
            if (interactionButtonPressed)
            {
                interactor.StartInteraction();
                OnInterationStart(interactor);
            }
        }
        public void OnInterationStart(Interactor interactor)
        {
            currentInteractor = interactor;
            onInteractionStart.Invoke(interactor);
            interactor.StartInteraction();
        }
        public void OnInterationEnd(Interactor interactor)
        {
            currentInteractor = null;
            onInteractionEnd.Invoke(interactor);
            interactor.EndInteraction();
        }
        void Update()
        {
            if (currentInteractor)
            {
                var InteractionUp = currentInteractor.Mapper.InputManager.GetButtonDown(currentInteractor.Mapper.Hand, InterActionButton);
                if (currentInteractor)
                {
                    OnInterationEnd(currentInteractor);
                }
            }
        }
    }
}
