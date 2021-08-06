﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    public enum ControllerType
    {
        Normal,
        Knuckles
    }
    [CreateAssetMenu(menuName = "Kandooz/KVR/Axis VR Input Manager")]
    public class UnityAxisBasedVRInputManager : AbstractVRInputManager
    {
        public ControllerType type;
        [Header("Left hand")]
        [HideInInspector] [SerializeField] private string leftTrigger = "KVR_left_trigger";
        [HideInInspector] [SerializeField] private string leftGrip = "KVR_left_grip";
        [HideInInspector] [SerializeField] private string leftThumb = "KVR_left_thumb";

        [Header("Left hand")]
        [HideInInspector] [SerializeField] private string leftIndex = "KVR_left_index";
        [HideInInspector] [SerializeField] private string leftMiddle = "KVR_left_middle";
        [HideInInspector] [SerializeField] private string leftRing = "KVR_left_ring";
        [HideInInspector] [SerializeField] private string leftPinky = "KVR_left_pinky";

        [Header("Right hand ")]
        [HideInInspector] [SerializeField] private string rightTrigger = "KVR_right_trigger";
        [HideInInspector] [SerializeField] private string rightGrip = "KVR_right_grip";
        [Header("Right hand")]
        [HideInInspector] [SerializeField] private string rightIndex = "KVR_right_index";
        [HideInInspector] [SerializeField] private string rightMiddle = "KVR_right_middle";
        [HideInInspector] [SerializeField] private string rightRing = "KVR_right_ring";
        [HideInInspector] [SerializeField] private string rightPinky = "KVR_right_pinky";
        [HideInInspector] [SerializeField] private string rightThumb = "KVR_right_thumb";



        public string LeftTrigger { get => leftTrigger; }
        public string LeftGrip { get => leftGrip; }
        public string LeftThumb { get => leftThumb; }
        public string LeftIndex { get => leftIndex;  }
        public string LeftMiddle { get => leftMiddle;}
        public string LeftRing { get => leftRing; }
        public string LeftPinky { get => leftPinky;}
        public string RightTrigger { get => rightTrigger; }
        public string RightGrip { get => rightGrip; }
        public string RightIndex { get => rightIndex;}
        public string RightMiddle { get => rightMiddle; }
        public string RightRing { get => rightRing; }
        public string RightPinky { get => rightPinky;  }
        public string RightThumb { get => rightThumb; }

        public override float GetFingerValue(HandType hand, FingerName finger)
        {
            var value = 0f;
            switch (type)
            {
                case ControllerType.Normal:
                    value = GetNormalInput(finger, hand);

                    break;
                case ControllerType.Knuckles:
                    value = GetKnucklesInput(finger,hand);
                    break;
                default:
                    break;
            }
            return value;
        }
        private float GetNormalInput ( FingerName finger, HandType hand)
        {
            float value=0;
            switch (finger)
            {
                case FingerName.Thumb:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(LeftThumb);
                        if (value < .1f)
                        {
                            if (Input.GetKey(KeyCode.JoystickButton8)||
                                Input.GetKey(KeyCode.JoystickButton2) ||
                                Input.GetKey(KeyCode.JoystickButton3))
                            {
                                value = 1;
                            }
                            else 
                            if (Input.GetKey(KeyCode.JoystickButton16) ||
                                Input.GetKey(KeyCode.JoystickButton12) ||
                                Input.GetKey(KeyCode.JoystickButton13))
                            {
                                value = .5f;
                            }
                        }
                    }
                    else
                    {
                        value = Input.GetAxis(RightThumb);
                        if (value < .1f)
                        {
                            if (
                                Input.GetKey(KeyCode.JoystickButton9)||
                                Input.GetKey(KeyCode.JoystickButton0)||
                                Input.GetKey(KeyCode.JoystickButton1)
                                )
                            {
                                value = 1;
                            }
                            else if (
                                Input.GetKey(KeyCode.JoystickButton17)||
                                Input.GetKey(KeyCode.JoystickButton10)||
                                Input.GetKey(KeyCode.JoystickButton11)
                                )
                            {
                                value = .5f;
                            }
                        }
                    }
                    break;
                case FingerName.Index:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(leftTrigger);
                    }
                    else
                    {
                        value = Input.GetAxis(RightTrigger);
                    }
                    break;

                case FingerName.Middle:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(leftGrip);
                    }
                    else
                    {
                        value = Input.GetAxis(rightGrip);
                    }
                    break;
                case FingerName.Ring:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(leftGrip);
                    }
                    else
                    {
                        value = Input.GetAxis(rightGrip);
                    }
                    break;
                case FingerName.Pinky:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(LeftGrip);
                    }
                    else
                    {
                        value = Input.GetAxis(RightGrip);
                    }
                    break;
                default:
                    break;
            }
            return value;
        }
        private float GetKnucklesInput(FingerName finger, HandType hand)
        {
            float value=0;
            switch (finger)
            {
                case FingerName.Thumb:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(LeftThumb);
                        if (value < .1f)
                        {
                            if (Input.GetKey(KeyCode.JoystickButton8))
                            {
                                value = 1;
                            }
                            else if (Input.GetKey(KeyCode.JoystickButton16))
                            {
                                value = .5f;
                            }
                        }
                    }
                    else
                    {
                        value = Input.GetAxis(RightThumb);
                        if (value < .1f)
                        {
                            if (Input.GetKey(KeyCode.JoystickButton9))
                            {
                                value = 1;
                            }
                            else if (Input.GetKey(KeyCode.JoystickButton17))
                            {
                                value = .5f;
                            }
                        }
                        Debug.Log(value);
                    }
                    break;
                case FingerName.Index:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(LeftIndex);
                    }
                    else
                    {
                        value = Input.GetAxis(RightIndex);
                    }
                    break;

                case FingerName.Middle:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(LeftMiddle);
                    }
                    else
                    {
                        value = Input.GetAxis(RightMiddle);
                    }
                    break;
                case FingerName.Ring:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(LeftRing);
                    }
                    else
                    {
                        value = Input.GetAxis(RightRing);
                    }
                    break;
                case FingerName.Pinky:
                    if (hand == HandType.left)
                    {
                        value = Input.GetAxis(LeftPinky);
                    }
                    else
                    {
                        value = Input.GetAxis(RightPinky);
                    }
                    break;
                default:
                    break;
            }
            return value;
        }
        public override bool GetFingerDown(HandType handType, FingerName finger)
        {
            /// Todo Add other buttons
            bool value = false;
            switch (finger)
            {
                case FingerName.Thumb:
                    break;
                case FingerName.Index:
                    value = (handType == HandType.right) ? Input.GetButtonDown(rightTrigger) : Input.GetButtonDown(leftTrigger);
                    break;
                case FingerName.Middle:
                    break;
                case FingerName.Ring:
                    break;
                case FingerName.Pinky:
                    break;
                case FingerName.Grip:
                    value= (handType == HandType.right) ? Input.GetButtonDown(rightGrip) : Input.GetButtonDown(LeftGrip);
                    break;
            }
            return value;
        }
        public override bool GetFinger(HandType handType, FingerName finger)
        {
            /// Todo Add other buttons
            /// Todo use Buttons instead of axis
            bool value = false;
            switch (finger)
            {
                case FingerName.Thumb:
                    break;
                case FingerName.Index:
                    break;
                case FingerName.Middle:
                    break;
                case FingerName.Ring:
                    break;
                case FingerName.Pinky:
                    break;
                case FingerName.Trigger:
                    value = (handType == HandType.right) ? Input.GetAxis(rightTrigger)>.2f : Input.GetAxis(leftTrigger) > .2f;
                    break;
                case FingerName.Grip:
                    value = (handType == HandType.right) ? Input.GetAxis(rightGrip)>.2f : Input.GetAxis(LeftGrip)>.2f;
                    break;
            }
            return value;
        }
        public override  bool GetFIngerUp(HandType handType, FingerName finger)
        {
            /// Todo Add other buttons
            bool value = false;
            switch (finger)
            {
                case FingerName.Thumb:
                    break;
                case FingerName.Index:
                    value = (handType == HandType.right) ? Input.GetButtonUp(rightTrigger) : Input.GetButtonDown(leftTrigger);
                    break;
                case FingerName.Middle:
                    break;
                case FingerName.Ring:
                    break;
                case FingerName.Pinky:
                    break;
                case FingerName.Grip:
                    value = (handType == HandType.right) ? Input.GetButtonUp(rightGrip) : Input.GetButtonDown(LeftGrip);
                    break;
            }
            return value;
        }
    }
}