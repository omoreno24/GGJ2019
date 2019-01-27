using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IInteractor
{
    public Transform PickUpPoint;

    public IInteractable Item;

    public IInteractable SelectedInteractable;

    public IInteractable CurrentItem { get { return Item; } }

    public PlayerMovement MovementController;
    public DefaultInput Control;

    private Vector3 Motion;

    Animator _animator;
    public ParticleSystem Smoke;

    private void OnEnable()
    {
        Control.Enable();
    }

    private void OnDisable()
    {
        Control.Disable();
    }

    private void Awake()
    {
        Control.PlayerAction.Movement.performed += (ctx) => MoveEvent(ctx.ReadValue<Vector2>());
        Control.PlayerAction.Interact.performed += (ctx) => InteractEvent();

        _animator = GetComponent<Animator>();
    }

    void MoveEvent(Vector2 directions)
    {
        Motion = new Vector3(directions.x, 0, directions.y);

        if (Motion.sqrMagnitude > 0)
        {
            _animator.Play("CharacterWalk");
            if (!Smoke.isPlaying)
                Smoke.Play();
        }
        else
        {
            if (Smoke.isPlaying)
                Smoke.Stop();
            _animator.Play("CharacterIDLE");
        }
    }

    void InteractEvent()
    {
        if (SelectedInteractable is null)
        {
            DropItem();
        }
        else
        {
            SelectedInteractable.Interact(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovementController.Move(Motion);
    }

    public void PickUP(IInteractable interactable)
    {
        if (this.Item == null)
        {
            this.Item = interactable;

            interactable.GameObject.transform.position = PickUpPoint.position;
            interactable.GameObject.transform.rotation = PickUpPoint.rotation;

            interactable.GameObject.transform.SetParent(this.transform);

            this.SelectedInteractable = null;
        }
    }

    public void DropItem()
    {
        if (Item is null)
            return;

        Item.GameObject.transform.SetParent(null);
        Item.Enable();

        this.Item = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "interactable")
        {
            SelectedInteractable = other.gameObject.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "interactable")
        {
            var interactable = other.gameObject.GetComponent<IInteractable>();

            if (interactable != null && SelectedInteractable != null)
            {
                SelectedInteractable = interactable == SelectedInteractable ? null : SelectedInteractable;
            }
        }
    }
}
