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
            interactable = GetComponent<Interactable>();
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
        private HandConstraintData GetHand(HandSource hand)
        {
            return hand == HandSource.Left ? leftHand : rightHand;
        }
    }
}
