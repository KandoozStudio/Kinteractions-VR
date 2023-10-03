using Kandooz.InteractionSystem.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.InteractionSystem.Core
{
    [RequireComponent(typeof(InteractableBase))]
    public class MaterialHighlighter : MonoBehaviour
    {
        private Renderer[] renderers;
        private Color []color;
        private void Awake()
        {
            renderers = GetComponentsInChildren<MeshRenderer>();
            color = new Color[renderers.Length];
            for (int i = 0; i < renderers.Length; i++)
            {
                color[i] = renderers[i].material.color;

            }
            color= new Color[renderers.Length];
            var interactable=GetComponent<InteractableBase>();
            interactable.OnHoverStarted += (interactor) =>
            {
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].material.color = color[i] * .3f;

                }
            };
            interactable.OnHoverEnded += (interactor) =>
            {
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].material.color = color[i];

                }
            };
        }
    }

}
