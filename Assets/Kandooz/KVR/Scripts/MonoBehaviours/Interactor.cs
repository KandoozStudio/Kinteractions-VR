using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kandooz.KVR
{
    [RequireComponent(typeof(HandInputMapper))]
    public class Interactor : MonoBehaviour
    {
        HandInputMapper mapper;

        private void Awake()
        {
            mapper = GetComponent<HandInputMapper>();
        }
    }
}
