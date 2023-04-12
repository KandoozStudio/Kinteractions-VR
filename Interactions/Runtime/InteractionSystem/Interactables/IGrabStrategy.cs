namespace Kandooz.Interactions.Runtime
{
    public interface IGrabStrategy
    {
        void Grab(InteractableBase interactable, InteractorBase interactor);
        void UnGrab(InteractableBase interactable, InteractorBase interactor);
    }
}