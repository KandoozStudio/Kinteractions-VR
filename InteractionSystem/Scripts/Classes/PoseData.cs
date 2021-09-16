using System;
using UnityEngine;

namespace Kandooz.KVR
{
    [Serializable]
    public struct PoseData
    {
        public AnimationClip open;
        public AnimationClip closed;
        public PoseType type;
        [SerializeField] private string name;

        public string Name
        {
            get {
                if (name != "")
                {
                    return name;
                }
                if (type == PoseType.Static)
                {
                    return open.name;
                }
                return $"{open.name}--{closed.name}";
            }
            set => name = value;
        }

        public enum PoseType
        {
            Static, Tweenable
        }
        public void SetPosNameIfEmpty(string name)
        {
            if (this.Name == "")
            {
                this.Name = name;
            }
        }
    }
}
