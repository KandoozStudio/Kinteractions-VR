using Kandooz.InteractionSystem.Core;

namespace Kinteractions_VR.Runtime.Animations.Constraints
{
    public interface IPoseConstrainer
    {
        public HandPoseConstraints LeftPoseConstraints {get;}
        public HandPoseConstraints RightPoseConstraints {get;}
    }
}