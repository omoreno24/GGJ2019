using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    public void Enable()
    {
        this.gameObject.GetComponent<Collider>().enabled = true;
    }

    public void Disable()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
    }
}
