using Kandooz.InteractionSystem.Core;
using UnityEngine;
namespace Kandooz.InteractionSystem.Animations
{
    public class InteractionPoseConstrainer : MonoBehaviour
    {
        [SerializeField] private HandConstraints rightHand;
        [SerializeField] private HandConstraints leftHand;
        //private Interactable interactable;
        private HandConstraints currentConstraints;
        public HandConstraints CurrentConstraints { get => currentConstraints;}
        private void Start()
        {
            GetDependencies();
            SubscribeToInteractableEvents();
            IntilaizeConstrainsRelativeTransform();
        }
        private void IntilaizeConstrainsRelativeTransform()
        {
            if (!rightHand.relativeTransform)
            {
                InitializeRelativeTransform(rightHand);
            }
            if (!leftHand.relativeTransform)
            {
                InitializeRelativeTransform(leftHand);
            }
        }
        private void InitializeRelativeTransform(HandConstraints hand)
        {
            hand.relativeTransform = new GameObject().transform;
            hand.relativeTransform.parent = this.transform;
            hand.relativeTransform.localPosition = Vector3.zero;
            hand.relativeTransform.localRotation = Quaternion.identity;
        }
        private void GetDependencies()
        {
            //interactable = GetComponent<Interactable>();
        }
        private void SubscribeToInteractableEvents()
        {
            //interactable.onInteractionStart.AddListener(OnInteractionStart);
            //interactable.onInteractionEnd.AddListener(OnInteractionEnd);
        }
        // private void OnInteractionStart(Interactor interactor)
        // {
        //     currentConstraints = GetHand(interactor.Mapper.Hand);
        //     interactor.Mapper.Constraints = CurrentConstraints.constraints;
        // }
        // private void OnInteractionEnd(Interactor interactor)
        // {
        //     interactor.Mapper.Constraints = HandConstraints.Free;
        // }
        public  HandConstraints GetHand(HandIdentifier hand)
        {
            return hand == HandIdentifier.Left ? leftHand : rightHand;
        }
    }
}
