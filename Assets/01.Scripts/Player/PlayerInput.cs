using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> Movement;
    public UnityEvent jump;

    Camera main;

    public PlayerInputAction PlayerInputAction { get; private set; }

    private void Awake()
    {
        main = Camera.main;
        PlayerInputAction = new PlayerInputAction();
        PlayerInputAction.Enable();
        //PlayerInputAction.PlayerAction.MoveVector.performed += MovementPerform;
        PlayerInputAction.PlayerAction.Jump.performed += JumpPerform;
    }

    private void Update()
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
