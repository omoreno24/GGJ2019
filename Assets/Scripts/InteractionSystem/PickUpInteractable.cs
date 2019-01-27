using UnityEngine;
using System.Collections;

public class PickUpInteractable : Interactable, IInteractable
{
    [SerializeField]
    private int id = 1;

    public string ID => $"PICKUP_{id}";

    public GameObject GameObject => gameObject;

    public void Interact(IInteractor interactor)
    {
        interactor.PickUP(this);
        Disable();
    }
}
