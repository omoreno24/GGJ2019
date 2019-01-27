using System;
using UnityEngine;

public interface IInteractable
{
    string ID
    {
        get;
    }

    GameObject GameObject
    {
        get;
    }

    void Interact(IInteractor interactor);

    void Enable();

    void Disable();
}
