using UnityEngine;
using System.Collections;

public class ItemSlotInteractableWithWork: MonoBehaviour, IInteractable
{
    public enum State
    {
        Empty,
        Filled,
        Ready,
        Completed
    }
    public int RemainingHitCount;
    public State CurrentState;
    public GameObject Product;
    public GameObject ResultProduct;
    public Transform ResultPoint;
    public string ItemRequestID;
    public string ID => name;

    public GameObject GameObject => gameObject;

    private IInteractable resultInteractable;
    public void Disable()
    {
    }

    public void Interact(IInteractor interactor)
    {

        switch(CurrentState)
        {
            case State.Empty:
                if (interactor.CurrentItem is null)
                    return;

                if(string.IsNullOrWhiteSpace(ItemRequestID) || ItemRequestID.ToLower() == interactor.CurrentItem.ID.ToLower())
                {
                    Product = interactor.CurrentItem.GameObject;

                    interactor.DropItem();

                    Product.transform.SetParent(this.transform);
                    Product.transform.position = ResultPoint.transform.position;
                    Product.transform.rotation = ResultPoint.transform.rotation;

                    CurrentState = State.Filled;
                }

                break;
            case State.Filled:
                if (RemainingHitCount > 0)
                    RemainingHitCount -= 1;
                else
                {
                    CurrentState = State.Ready;
                    Destroy(Product);

                    if (ResultProduct is null || ResultProduct.tag != "interactable")
                        return;

                    resultInteractable = Instantiate(ResultProduct, ResultPoint.transform.position, ResultPoint.transform.rotation).GetComponent<IInteractable>();

                    CurrentState = State.Ready;
                }
                break;
            case State.Ready:
                interactor.PickUP(resultInteractable);
                CurrentState = State.Completed;
                break;
            case State.Completed:
                break;
        }
    }
}
