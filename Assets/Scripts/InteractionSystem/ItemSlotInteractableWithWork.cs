using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemSlotInteractableWithWork: Interactable, IInteractable
{
    public enum State
    {
        Empty,
        Filled,
        Ready,
        Completed
    }

    public float RemainingHitCount;
    public State CurrentState;
    public GameObject Product;
    public GameObject ResultProduct;
    public Transform ResultPoint;
    public string ItemRequestID;
    public string ID => name;

    public GameObject GameObject => gameObject;

    private IInteractable resultInteractable;
    private float DefaultHealthValue;

    public Image UIfiller;

    void Start()
    {
        Debug.Log("Start");
        DefaultHealthValue = RemainingHitCount;
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
                {
                    UIfiller.transform.parent.gameObject.SetActive(true);
                    RemainingHitCount -= 1;
                    Debug.Log(RemainingHitCount / DefaultHealthValue);
                    UIfiller.fillAmount = 1-(RemainingHitCount / DefaultHealthValue);
                }
                else
                {
                    UIfiller.transform.parent.gameObject.SetActive(false);
                    CurrentState = State.Ready;
                    Destroy(Product);

                    if (ResultProduct is null || ResultProduct.tag != "interactable")
                        return;

                    resultInteractable = Instantiate(ResultProduct, ResultPoint.transform.position, ResultPoint.transform.rotation).GetComponent<IInteractable>();

                    CurrentState = State.Ready;
                }
                break;
            case State.Ready:
                resultInteractable.Interact(interactor);
                CurrentState = State.Completed;
                break;
            case State.Completed:
                break;
        }
    }
}
