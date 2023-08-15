using Kandooz.InteractionSystem.Core;

namespace Kandooz.InteractionSystem.Animations.Constraints
{
    public interface IPoseConstrainer
    {
        public PoseConstrains LeftPoseConstrains {get;}
        public PoseConstrains RightPoseConstrains {get;}
    }
}