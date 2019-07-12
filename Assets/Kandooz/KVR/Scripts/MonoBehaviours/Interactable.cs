using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{
    public class Interactable : MonoBehaviour
    {
        public HandData hand;
        public Material HoverMaterial;
        public new Renderer renderer;
        private Material originalMaterial;
        void OnEnable()
        {
            if (!renderer )
            {
                renderer=this.GetComponentInChildren<Renderer>();
                originalMaterial = renderer.material;
            }
        }
        public void OnHandHover()
        {
        }

        public void OnGrabStart()
        {

        }
        public void OnGrabEnd()
        {
            
        }
    }
}