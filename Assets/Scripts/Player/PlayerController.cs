using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public  PlayerMovement movementController;

    public DefaultInput control;
    private Vector3 motion;

    private void OnEnable()
    {
        control.Enable();
    }

    private void OnDisable()
    {
        control.Disable();
    }

    private void Awake()
    {
        control.PlayerAction.Movement.performed += (ctx) => MoveEvent(ctx.ReadValue<Vector2>());
    }

    void MoveEvent(Vector2 directions)
    {
        this.motion = new Vector3(directions.x, 0, directions.y);
        Debug.Log("It's Working");
    }

    // Update is called once per frame
    void Update()
    {
        movementController.Move(motion);
    }


    
}
