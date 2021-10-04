using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [RequireComponent(typeof(Interactable))]
    public class InteractionPoseConstrainer : MonoBehaviour
    {
        [SerializeField] private HandConstraintData rightHand;
        [SerializeField] private HandConstraintData leftHand;
        private Interactable interactable;
        private HandConstraintData currentConstraints;
        public HandConstraintData CurrentConstraints { get => currentConstraints;}
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
        private void InitializeRelativeTransform(HandConstraintData hand)
        {
            hand.relativeTransform = new GameObject().transform;
            hand.relativeTransform.parent = this.transform;
            hand.relativeTransform.localPosition = Vector3.zero;
            hand.relativeTransform.localRotation = Quaternion.identity;
        }
        private void GetDependencies()
        {
            interactable = GetComponent<Interactable>();
        }
        private void SubscribeToInteractableEvents()
        {
            interactable.onInteractionStart.AddListener(OnInteractionStart);
            interactable.onInteractionEnd.AddListener(OnInteractionEnd);
        }
        private void OnInteractionStart(Interactor interactor)
        {
            currentConstraints = GetHand(interactor.Mapper.Hand);
            interactor.Mapper.Constraints = CurrentConstraints.constraints;
        }
        private void OnInteractionEnd(Interactor interactor)
        {
            interactor.Mapper.Constraints = HandConstraints.Free;
        }
        public  HandConstraintData GetHand(HandSource hand)
        {
            return hand == HandSource.Left ? leftHand : rightHand;
        }
    }
}
