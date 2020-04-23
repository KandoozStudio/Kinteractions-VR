using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kandooz.KVR
{

    [RequireComponent(typeof(Interactable))]
    public class HoverHighlighter : MonoBehaviour
    {
        public Shader hoverShader;
        public Renderer []renderersToHighlight;
        private Shader[] defaultShaders;
        void Start()
        {
            if (renderersToHighlight == null|| renderersToHighlight.Length==0)
            {
                renderersToHighlight = GetComponentsInChildren<Renderer>();
            }
            defaultShaders = new Shader[renderersToHighlight.Length];
            for (int i = 0; i < renderersToHighlight.Length; i++)
            {
                defaultShaders[i]= renderersToHighlight[i].material.shader;
            }
            var interactable = GetComponent<Interactable>();
            interactable.onHandHoverStart.AddListener((hand) => {
                for (int i = 0; i < renderersToHighlight.Length; i++)
                {
                    renderersToHighlight[i].material.shader = hoverShader;
                }
            });
            interactable.onHandHoverEnd.AddListener((hand) => {
                for (int i = 0; i < renderersToHighlight.Length; i++)
                {
                    renderersToHighlight[i].material.shader= defaultShaders[i];
                }


            });

        }
    }
}