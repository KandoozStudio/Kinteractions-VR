using Kandooz.KVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [RequireComponent(typeof(Interactable))]
    public class HoverPoseConstrainter : MonoBehaviour
    {
        [SerializeField] private Handconstrainter leftHandConstraints;
        [SerializeField] private Handconstrainter rightHandConstraints;
        private Interactable interactable;

        private void Start()
        {
            interactable = GetComponent<Interactable>();
            interactable.onHoverStart.AddListener(OnHoverStart);
            interactable.onHoverEnd.AddListener(OnHoverEnd);
        }

        private void OnHoverStart(Interactor interactor)
        {
            var constranier = SelectConstrainer(interactor.Mapper.Hand);
            constranier.ConstraintHand(interactor.Mapper);
        }
        private void OnHoverEnd(Interactor interactor)
        {
            var constranier = SelectConstrainer(interactor.Mapper.Hand);
            constranier.UnConstraintHand(interactor.Mapper);
        }

        private Handconstrainter SelectConstrainer(HandSource hand)
        {
            return (hand == HandSource.Left ? leftHandConstraints : rightHandConstraints);
        }
    }
}