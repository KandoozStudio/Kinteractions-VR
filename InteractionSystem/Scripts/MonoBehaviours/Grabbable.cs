using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    [RequireComponent(typeof(InteractionPoseConstrainer))]
    public class Grabbable : MonoBehaviour
    {
        [SerializeField] private VariableTweener tweener;
        private InteractionPoseConstrainer constrianer;
        private Interactable interactable;
        private Rigidbody body;
        private TweenableVector vectorTweener;
        private TweenableQuatrinion quatrenionTweener;
        private void Start()
        {
            GetDependencies();
            SubscripeToInteractableEvents();
        }
        private void SubscripeToInteractableEvents()
        {
            interactable.onInteractionStart.AddListener(OnInteractionStart);
            interactable.onInteractionEnd.AddListener(OnInteractionEnd);
        }
        private void GetDependencies()
        {
            interactable = GetComponent<Interactable>();
            constrianer = GetComponent<InteractionPoseConstrainer>();
            tweener = GetComponent<VariableTweener>();
            tweener ??= gameObject.AddComponent<VariableTweener>();
            body = GetComponent<Rigidbody>();
        }
        private void OnInteractionStart(Interactor interactor)
        {
            InitializeTweeners();
            var hand = constrianer.GetHand(interactor.Mapper.Hand);
            Vector3 point = CalculateRelativePositionToHand(interactor, hand.relativeTransform);
            Quaternion rotation = hand.relativeTransform.localRotation;
            body.isKinematic = true;

            StartTweeners(interactor, point, rotation);
        }
        private void StartTweeners(Interactor interactor, Vector3 point, Quaternion rotation)
        {
            StartRotationTweener(rotation);
            StartPositionTweener(interactor, point);
        }

        private void StartPositionTweener(Interactor interactor, Vector3 point)
        {
            vectorTweener.Value = point;
            vectorTweener.onChange += (position) => { transform.position = position; };
            vectorTweener.onFinished += () =>
            {
                body.position = point;
                this.transform.parent = interactor.transform;
            };
        }

        private void StartRotationTweener(Quaternion rotation)
        {
            quatrenionTweener.Value = rotation;
            quatrenionTweener.onChange += (rot) => { this.transform.rotation = rot; };
            quatrenionTweener.onFinished += () => { transform.rotation = rotation; };
        }

        private void InitializeTweeners()
        {
            vectorTweener = new TweenableVector(tweener, this.transform.position);
            quatrenionTweener = new TweenableQuatrinion(tweener, this.transform.rotation);
        }

        private Vector3 CalculateRelativePositionToHand(Interactor interactor, Transform relativeTransform)
        {
            var positionToHandVector = interactor.transform.position - relativeTransform.position;
            return positionToHandVector + this.transform.position;
        }

        private void OnInteractionEnd(Interactor interactor)
        {

        }
        
    }
}