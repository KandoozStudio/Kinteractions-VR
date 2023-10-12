using Kandooz.InteractionSystem.Animations;
using Kandooz.InteractionSystem.Core;
using UnityEngine;

namespace Kandooz.InteractionSystem.Interactions
{
    public class LinearConstraintInteractable : InteractableBase
    {
        //todo: editor
        [SerializeField] private Transform point1;
        [SerializeField] private Transform point2;
        [SerializeField] private Transform interactableObject;
        [SerializeField] private HandPoseController leftHand;
        [SerializeField] private HandPoseController rightHand;
        private (Vector3 position, Quaternion rotation) leftHandPivot;
        private (Vector3 position, Quaternion rotation) rightHandPivot;
        private (Vector3 position, Quaternion rotation) currentPivot;
        private Transform fakeHandTransform;
        private float t;
        protected override void Select()
        {
                CurrentInteractor.ToggleHandModel(false);
                fakeHandTransform = (CurrentInteractor.HandIdentifier == HandIdentifier.Left ? leftHand : rightHand).transform;
                currentPivot = (CurrentInteractor.HandIdentifier == HandIdentifier.Left ? leftHandPivot : rightHandPivot);
                fakeHandTransform.position = CurrentInteractor.transform.position;
                fakeHandTransform.rotation = CurrentInteractor.transform.rotation;
                fakeHandTransform.gameObject.SetActive(true);
                t = 0;
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
        protected override void EndHover(){}
        private void Awake()
        {
            leftHandPivot = new()
            {
                position = leftHand.transform.localPosition
                ,
                rotation = leftHand.transform.localRotation
            };
            rightHandPivot = new()
            {
                position = rightHand.transform.localPosition,
                rotation = rightHand.transform.localRotation
            };
        }

        private void Update()
        {
            if (!IsSelected) return;
            interactableObject.transform.position = 
                GetPositionBetweenTwoPoints(
                    CurrentInteractor.transform.position,
                    point1.position,
                    point2.position
                    );
            SetInteractionHandPosition();
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
            projectedPoint = GetPositionBetweenTwoPoints(projectedPoint,position1,position2);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(projectedPoint,.03f);

        }
        private void SetInteractionHandPosition()
        {
            t += Time.deltaTime * 10;
            fakeHandTransform.localPosition = Vector3.Lerp(fakeHandTransform.localPosition, currentPivot.position, t);
            fakeHandTransform.localRotation = Quaternion.Lerp(fakeHandTransform.localRotation, currentPivot.rotation, t);


        }

        private static Vector3 GetPositionBetweenTwoPoints(Vector3 point,Vector3 start,Vector3 end)
        {
            var direction = (end - start);
            var projectedPoint = Vector3.Project(point - start, direction) + start;
            var normalizedDistance = FindNormalizedDistanceAlongPath(direction, projectedPoint, start);
            return Vector3.Lerp(start, end, normalizedDistance);
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