public interface IInteractor
{
    void PickUP(IInteractable interactable);

    void DropItem();

    IInteractable CurrentItem
    {
        get;
    }
}
