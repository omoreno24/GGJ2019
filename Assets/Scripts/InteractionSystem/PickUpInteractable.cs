using UnityEngine;
using System.Collections;

public class PickUpInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int id = 1;

    public string ID => $"PICKUP_{id}";

    public GameObject GameObject => gameObject;

    public void Disable()
    {
        this.GetComponent<Collider>().enabled = false;
    }

    public void Interact(IInteractor interactor)
    {
        interactor.PickUP(this);
    }
}
