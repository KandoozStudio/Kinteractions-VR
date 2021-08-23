
using UnityEngine;

namespace Kandooz.KVR
{
    public class Interactable : MonoBehaviour
    {
        [HideInInspector] [SerializeField] HandData data;
        [HideInInspector] [SerializeField] HandConstrains LeftHandConstraints;
        [HideInInspector] [SerializeField] HandConstrains righHandConstraints;

        [HideInInspector] [SerializeField] bool constraintOnhover=false;
        [HideInInspector] [SerializeField] HandConstrains LeftHandHoverConstraints;
        [HideInInspector] [SerializeField] HandConstrains righHandHoverConstraints;


        IPose pose;
        public IPose Pose { get => pose; set => pose = value; }


        public void OnHoverStart(Interactor interactor)
        {

        }
        public void OnHoverEnd(Interactor interactor)
        {

        }
        public void OnInterationStart(Interactor interactor)
        {

        }
        public void OnInterationEnd(Interactor interactor)
        {

        }


    }
}
