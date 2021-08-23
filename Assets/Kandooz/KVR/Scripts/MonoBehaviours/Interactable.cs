
using UnityEngine;

namespace Kandooz.KVR
{
    public class Interactable : MonoBehaviour
    {
        [HideInInspector] [SerializeField] HandData data;
        [HideInInspector] [SerializeField] Button button;
        [HideInInspector] [SerializeField] HandConstrains LeftHandConstraints;
        [HideInInspector] [SerializeField] HandConstrains righHandConstraints;

        [HideInInspector] [SerializeField] bool constraintOnhover=false;
        [HideInInspector] [SerializeField] HandConstrains LeftHandHoverConstraints;
        [HideInInspector] [SerializeField] HandConstrains righHandHoverConstraints;

        [HideInInspector] [SerializeField] Interactor currentInteractor;
        IPose pose;
        public IPose Pose { get => pose; set => pose = value; }

        public void OnHoverStart(Interactor interactor)
        {

        }
        public void OnHoverEnd(Interactor interactor)
        {

        }
        public void OnHoverUpdate(Interactor interactor)
        {
            var interactionButtonPressed=interactor.Mapper.InputManager.GetButtonDown(interactor.Mapper.Hand, button);
            if (interactionButtonPressed)
            {
                interactor.StartInteraction();
                OnInterationStart(interactor);
            }
        }
        public void OnInterationStart(Interactor interactor)
        {
            interactor.StartInteraction();
            currentInteractor = interactor;
        }
        public void OnInterationEnd(Interactor interactor)
        {
            interactor.EndInteraction();
            currentInteractor = null;
        }
        void Update()
        {
            if (currentInteractor)
            {
            var InteractionUp=currentInteractor.Mapper.InputManager.GetButtonDown(currentInteractor.Mapper.Hand, button);
                if (currentInteractor)
                {
                    OnInterationEnd(currentInteractor);
                }
            }
        }
    }
}
