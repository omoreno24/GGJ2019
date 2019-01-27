using UnityEngine;
using System.Collections;

public class ItemSlotInteractable : Interactable, IInteractable
{
    public bool Completed;

    public Transform DropPoint;
    public string ItemRequestID;
    public string ID => name;

    public GameObject GameObject => gameObject;

    public void Interact(IInteractor interactor)
    {
        if(interactor.CurrentItem is null || Completed)
            return;

        if(string.IsNullOrWhiteSpace(ItemRequestID) || interactor.CurrentItem.ID.ToLower() == ItemRequestID.ToLower())
        {
            var itemTransform = interactor.CurrentItem.GameObject.transform;

            interactor.DropItem();

            itemTransform.SetParent(this.transform);
            itemTransform.position = DropPoint.transform.position;
            itemTransform.rotation = DropPoint.transform.rotation;

            Completed = true;
        }
    }
}
