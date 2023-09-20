using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> Movement;
    public UnityEvent jump;

    public PlayerInputAction PlayerInputAction { get; private set; }

    private void Awake()
    {
        PlayerInputAction = new PlayerInputAction();
        PlayerInputAction.Enable();
        PlayerInputAction.PlayerAction.Jump.performed += JumpPerform;
    }

    private void FixedUpdate()
    {
        Movement?.Invoke(PlayerInputAction.PlayerAction.MoveVector.ReadValue<Vector2>());
    }

    private void JumpPerform(InputAction.CallbackContext context)
    {
        jump?.Invoke();
    }


}
