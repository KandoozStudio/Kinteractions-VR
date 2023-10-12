using Kandooz.InteractionSystem.Animations;
using Kandooz.InteractionSystem.Core;
using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{
    public class LinearConstraintInteractable : InteractableBase
    {
        [SerializeField] private VariableTweener tweener;
        [SerializeField] private Transform point1;
        [SerializeField] private Transform point2;
        [SerializeField] private Transform interactableObject;
        [SerializeField] private HandPoseController leftHand;
        [SerializeField] private HandPoseController rightHand;
        private (Vector3 position, Quaternion rotation) leftHandPivot;
        private (Vector3 position, Quaternion rotation) rightHandPivot;
        private Transform hand;

        protected override void Select()
        {
                CurrentInteractor.ToggleHandModel(false);
                hand = (CurrentInteractor.HandIdentifier == HandIdentifier.Left ? leftHand : rightHand).transform;
                hand.gameObject.SetActive(true);
                
                // Move to same position as Hand
                // Lerp the object to nearest possible position
                //Lerp the hand to local position and rotation of pivot

        }

        protected override void DeSelected()
        {
            CurrentInteractor.ToggleHandModel(true);
            leftHand.gameObject.SetActive(false);
            rightHand.gameObject.SetActive(false);
        }

        protected override void Activate()
        {
        }

        protected override void StartHover()
        {
        }

        protected override void EndHover()
        {
        }

        private void Update()
        {
            if(!IsSelected)return;
            var position2= point2.position;
            var position1 = point1.position;
            var direction = (position2 - position1);
            var projectedPoint = Vector3.Project(CurrentInteractor.transform.position - position1, direction) + position1;
            var t = FindNormalizedDistanceAlongPath(direction, projectedPoint, position1);
            projectedPoint = Vector3.Lerp(position1, position2, t);
            interactableObject.transform.position = projectedPoint;
        }


        private void Awake()
        {
            leftHandPivot = new()
            {
                position = leftHand.transform.position,
                rotation = leftHand.transform.rotation
            };
            rightHandPivot = new()
            {
                position = rightHand.transform.position,
                rotation = rightHand.transform.rotation
            };
        }

        private void OnDrawGizmos()
        {
            var position2= point2.position;
            var position1 = point1.position;
            var direction = (position2 - position1);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(position2,.03f);
            Gizmos.DrawSphere(position1,.03f);
            var projectedPoint = Vector3.Project(interactableObject.position - position1, direction) + position1;
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(projectedPoint,.03f);
            Gizmos.DrawLine(position2, position1);
            Gizmos.color=Color.blue;
            Gizmos.DrawLine(projectedPoint, interactableObject.position);
            var t = FindNormalizedDistanceAlongPath(direction, projectedPoint, position1);
            projectedPoint = Vector3.Lerp(position1, position2, t);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(projectedPoint,.03f);

        }

        private static float FindNormalizedDistanceAlongPath(Vector3 direction, Vector3 projectedPoint, Vector3 position1)
        {
            var axe = GetBiggestAxe(direction);
            var x = projectedPoint[axe];
            var m = 1 / direction[axe];
            var c = 0 - m * position1.x;
            var t = m * x + c;
            return t;
        }
        private static int GetBiggestAxe(Vector3 direction)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                return Mathf.Abs(direction.x) > Mathf.Abs(direction.z) ? 0 : 2;
            }
            return Mathf.Abs(direction.y) > Mathf.Abs(direction.z) ? 1 : 2;
        }
    }
}