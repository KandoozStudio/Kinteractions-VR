using Kandooz.InteractionSystem.Core;
using UnityEngine;

namespace Kinteractions_VR.Core.Runtime.Hand
{
    // TODO: Seperate the prefab Holders from eachother
    public interface IPoseable:IPoseablePrefabsHolder
    {
        public float this[int index]
        {
            get;
            set;
        }
        public float this[FingerName index]
        {
            get;
            set;
        }
        public int Pose
        {
            set;
        }
        void Initialize();

    }

    public interface IPoseablePrefabsHolder
    {
        IPoseable LeftHandPrefab { get; }
        IPoseable RightHandPrefab { get; }
    }
}