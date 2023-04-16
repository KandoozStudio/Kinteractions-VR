
using UnityEngine;

namespace Kandooz.Interactions.Runtime
{

    public class PhysicsHandFollwer : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float maxVelocity = 50;
        [SerializeField] private float maxDistance = 2;
        [SerializeField] private float minDistance = .02f;

        private float maxVelocitySqrt;
        private Rigidbody body;
        private float speed;

        private void Start()
        {
            maxVelocitySqrt = maxVelocity * maxVelocity;
            body = GetComponent<Rigidbody>();
            speed = 1 / Time.fixedDeltaTime;
            transform.parent = null;
            Teleport();
        }

        void FixedUpdate()
        {
            SetVelocity();
            SetAngularVelocity();
        }

        private void SetVelocity()
        {
            var velocityVector = target.transform.position - transform.position;
            if (velocityVector.sqrMagnitude > maxDistance || velocityVector.sqrMagnitude < minDistance)
            {
                Teleport();
                return;
            }

            var weightedSpeed = speed * Mathf.Lerp(0, 1, velocityVector.sqrMagnitude * 10000);
            body.velocity = velocityVector * weightedSpeed;
            body.velocity = Vector3.ClampMagnitude(body.velocity, maxVelocity);
        }

        private void SetAngularVelocity()
        {
            var relativeRotation = FindRelativeRotation(target.rotation, transform.rotation);
            relativeRotation.ToAngleAxis(out var angle, out var axis);
            var weightedSpeed = speed * Mathf.Lerp(0, 1, angle / 4);
            body.angularVelocity = axis * angle * Mathf.Deg2Rad * weightedSpeed;
        }

        private Quaternion FindRelativeRotation(Quaternion a, Quaternion b)
        {
            if (Quaternion.Dot(a, b) < 0)
            {
                b = new Quaternion(-b.x, -b.y, -b.z, -b.w);
            }

            return a * Quaternion.Inverse(b);
        }

        public void Teleport()
        {
            body.position = target.position;
            body.rotation = target.rotation;
            body.velocity = body.angularVelocity = Vector3.zero;
        }
    }
}