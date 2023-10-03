using System.Collections;
using System.Collections.Generic;
using Kandooz.InteractionSystem.Interactions;
using UnityEditor;
using UnityEngine;

namespace Kandooz.InteractionSystem.Core
{


    public class GameObjectMenuHelper : Editor
    {
        [MenuItem("GameObject/Kandooz/MakeGrabable ")]
        public static void MakeInteractable()
        {
            var obj = Selection.activeGameObject;
            if (obj == null)
                obj = new GameObject("grabable object");
            
            if(obj.GetComponent<Grabable>()) return;
            //obj.AddComponent<Rigidbody>();
            obj.AddComponent<Grabable>();
            Selection.activeGameObject = obj;
        }
        [MenuItem("GameObject/Kandooz/MakeThrowable")]
        public static void MakeThrowable()
        {
            var obj = Selection.activeGameObject;
            if (obj == null)
                obj = new GameObject("grabable object");
            
            if(obj.GetComponent<Throwable>()) return;
            obj.AddComponent<Rigidbody>().isKinematic=true;
            obj.AddComponent<Grabable>();
            obj.AddComponent<Throwable>();
            Selection.activeGameObject = obj;
        }
        [MenuItem("GameObject/Kandooz/Button")]
        public static void MakeButton()
        {
            var buttonObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder).transform;
            var buttonBody = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            buttonObject.GetComponent<MeshRenderer>().material.color = Color.red;
            buttonBody.name = "Button";
            var trigger = buttonBody.gameObject.AddComponent<BoxCollider>();
            trigger.center = Vector3.up * .2f;
            trigger.isTrigger = true;
            buttonObject.transform.parent = buttonBody.transform;
            buttonObject.localScale = new Vector3(.5f,.25f, .5f);
            buttonObject.localPosition = Vector3.up * .5f;
            var button = buttonBody.gameObject.AddComponent<VRButton>();
            button.Button = buttonObject.transform;
            buttonBody.localScale = Vector3.one / 10;

        }
        
    }
}