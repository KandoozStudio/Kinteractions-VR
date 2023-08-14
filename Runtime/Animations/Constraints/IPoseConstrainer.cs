using Kandooz.InteractionSystem.Core;

namespace Kinteractions_VR.Runtime.Animations.Constraints
{
    public interface IPoseConstrainer
    {
        public PoseConstrains LeftPoseConstrains {get;}
        public PoseConstrains RightPoseConstrains {get;}
    }
}