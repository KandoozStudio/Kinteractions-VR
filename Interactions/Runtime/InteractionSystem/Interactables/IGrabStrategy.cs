namespace Kandooz.Interactions.Runtime
{
    public interface IGrabStrategy
    {
        public void Initialize();
        void Grab(Gradable interactable, InteractorBase interactor);
        void UnGrab(Gradable interactable, InteractorBase interactor);

    }
}