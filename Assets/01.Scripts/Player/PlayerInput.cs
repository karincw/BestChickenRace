using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Packets;

public class PlayerInput : Player
{
    public UnityEvent<Vector2> Movement;
    public UnityEvent jump;

    public PlayerInputAction PlayerInputAction { get; private set; }

    Camera _cam;
    public LayerMask findObjLayer;
    private PlayerStateManager _playerStateManager;
    public bool InstallObj = false;

    public override void GameModePlay(Action action)
    {
        if (_playerStateManager.IsFinish is not true)
        {
            base.GameModePlay(action);
        }
        else
        {
            Movement?.Invoke(Vector2.zero);
        }
    }

    private void Awake()
    {
        _cam = Camera.main;
        PlayerInputAction = new PlayerInputAction();
        PlayerInputAction.Enable();
        PlayerInputAction.PlayerAction.Jump.performed += JumpPerform;
        PlayerInputAction.PlayerAction.MouseClick.performed += OnMouseLeft;
        PlayerInputAction.PlayerAction.MouseClickR.performed += OnMouseRight;

        _playerStateManager = FindAnyObjectByType<PlayerStateManager>();
    }

    private void FixedUpdate()
    {
        GameModePlay(() => Movement?.Invoke(PlayerInputAction.PlayerAction.MoveVector.ReadValue<Vector2>()));
        DeadModePlay(() => Movement?.Invoke(Vector2.zero));
    }

    private void JumpPerform(InputAction.CallbackContext context)
    {
        GameModePlay(() => jump?.Invoke());
    }
    private void OnMouseLeft(InputAction.CallbackContext callback)
    {
        InstallModePlay(() =>
        {
            if (_playerStateManager.clicked == true)
            {
                return;
            }
            Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0.01f, findObjLayer);

            if (hit.collider != null && hit.collider.CompareTag("Item"))
            {
                _playerStateManager.clickedItemName = hit.collider.gameObject.name;
                _playerStateManager.clicked = true;
                hit.collider.gameObject.SetActive(false);

                C_ItemSelectedPacket packet = new C_ItemSelectedPacket();
                packet.playerData.ItemSelected = true;
                NetworkManager.Instance.Send(packet);
            }
        });
    }
    private void OnMouseRight(InputAction.CallbackContext callback)
    {
        InGameUIManager.Instance.Uninstalling();
    }
}
