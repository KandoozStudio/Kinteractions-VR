using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kandooz.KVR
{
    [System.Serializable]
    public struct HandState
    {
        public int pose;
        public bool staticPose;
        public Vector2[] fingersMinMax;
    }
    public class Grabable : MonoBehaviour
    {
        public HandData data;
        [SerializeField] [HideInInspector] private Transform leftPivot;
        [SerializeField] [HideInInspector] private Transform rightPivot;
        [SerializeField] public HandState leftHand, rightHand;

        public void Initialize()
        {
            if (!leftPivot)
            {
                leftPivot = transform.Find("leftPivot");
                if (!leftPivot)
                {
                    leftPivot = new GameObject().transform;
                    leftPivot.parent = this.transform;
                    leftPivot.transform.position = Vector3.zero;
                    leftPivot.transform.rotation = Quaternion.identity;
                    leftPivot.name = "leftPivot";
                }
            }
            else
            {
                leftPivot.name = "leftPivot";
            }
            if (!rightPivot)
            {
                rightPivot = transform.Find("rightPivot");
                if (!rightPivot)
                {
                    rightPivot = new GameObject().transform;
                    rightPivot.parent = this.transform;
                    rightPivot.transform.position = Vector3.zero;
                    rightPivot.transform.rotation = Quaternion.identity;
                    rightPivot.name = "rightPivot";
                }
            }
            else
            {
                rightPivot.name = "rightPivot";

            }
        }
        public void OnGrabStart()
        {
            
        }
        public void OnGrabEnd()
        {

        }
    }
}