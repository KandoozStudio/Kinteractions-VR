namespace Kandooz.InteractionSystem.Core
{
    // TODO: Seperate the prefab Holders from eachother
    public interface IPoseable
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
        public PoseConstrains  Constrains { set; } 
    }
    
}