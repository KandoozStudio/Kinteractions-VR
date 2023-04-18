namespace Kandooz.InteractionSystem.Interactions
{
    public interface IGrabStrategy
    {
        public void Initialize();
        void Grab(Grabable interactable, InteractorBase interactor);
        void UnGrab(Grabable interactable, InteractorBase interactor);

    }
}