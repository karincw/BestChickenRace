using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> Movement;
    public UnityEvent jump;

    private bool _jumpPressed = false;
    Camera cam;

    public PlayerInputAction PlayerInputAction { get; private set; }

    private void Awake()
    {
        cam = Camera.main;
        PlayerInputAction = new PlayerInputAction();
        PlayerInputAction.Enable();
        //PlayerInputAction.PlayerAction.MoveVector.performed += MovementPerform;
        PlayerInputAction.PlayerAction.Jump.performed += JumpPerform;
    }

    private void FixedUpdate()
    {
        Movement?.Invoke(PlayerInputAction.PlayerAction.MoveVector.ReadValue<Vector2>());
    }

    //private void MovementPerform(InputAction.CallbackContext context)
    //{
    //    Movement?.Invoke(context.ReadValue<Vector2>());
    //}
    private void JumpPerform(InputAction.CallbackContext context)
    {
        jump?.Invoke();
    }


}
