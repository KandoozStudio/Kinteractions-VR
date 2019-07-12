using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kandooz.KVR
{
    public class Grabable : MonoBehaviour
    {
        
        public HandData data;
        [SerializeField] private bool editMode;
        [SerializeField] [HideInInspector] private GameObject handPosition;

        public void OnGrabStart()
        {

        }
        public void OnGrabEnd()
        {

        }
    }
}