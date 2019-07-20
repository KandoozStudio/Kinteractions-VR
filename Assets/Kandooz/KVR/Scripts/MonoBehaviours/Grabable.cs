using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kandooz.KVR
{
    public struct HandState
    {
        public int pose;
        public bool staticPose;
        public float[] fingers;
        public HandState(int pose,bool staticPose,float[]fingers)
        {
            this.pose = pose;
            this.staticPose = staticPose;
            this.fingers = fingers;
        }
    }
    public class Grabable : MonoBehaviour
    {
        public HandData data;
        [SerializeField] private bool editMode;
        [SerializeField] [HideInInspector] private HandType handToEdit;

        [SerializeField] [HideInInspector] private Transform leftPivot;
        [SerializeField] [HideInInspector] private Transform rightPivot;
        private HandState leftHand, rightHand;

        public HandState LeftHand
        {
            get
            {
                return leftHand;
            }

            set
            {
                leftHand = value;
            }
        }

        public HandState RightHand
        {
            get
            {
                return rightHand;
            }

            set
            {
                rightHand = value;
            }
        }

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