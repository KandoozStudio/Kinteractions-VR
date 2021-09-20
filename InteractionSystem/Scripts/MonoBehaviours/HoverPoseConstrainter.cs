using Kandooz.KVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [RequireComponent(typeof(Interactable))]
    public class HoverPoseConstrainter : MonoBehaviour
    {
        [SerializeField] private HandConstraints hoverConstraints = HandConstraints.Free;
        private Interactable interactable;
        private void Start()
        {
            interactable = GetComponent<Interactable>();
            interactable.onHoverStart.AddListener(OnHoverStart);
            interactable.onHoverEnd.AddListener(OnHoverEnd);
        }
        private void OnHoverStart(Interactor interactor)
        {
            interactor.Mapper.Constraints = hoverConstraints;
        }
        private void OnHoverEnd(Interactor interactor)
        {
            interactor.Mapper.Constraints = HandConstraints.Free;
        }


    }
}