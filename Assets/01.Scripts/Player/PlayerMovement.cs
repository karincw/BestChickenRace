using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("속도가 변할때")]
    public UnityEvent<float> OnVelocityChanged;
    public UnityEvent<bool> OnJumped;
    public UnityEvent<bool> OnWallLanding;

    private Rigidbody2D rig2d;

    private Vector2 _moveDirection;

    [Header("Movement")]
    [SerializeField] private float _currentSpeed = 0f;
    [SerializeField] private float _accelSpeed = 5f;
    [SerializeField] private float _maxSpeed = 4f;
    [SerializeField] private float _AddSpeed = 3f;

    [Header("Jump")]
    [SerializeField] private float _JumpPower = 0f;
    [SerializeField] private bool _canJumping = true;

    [Header("WallJump")]
    [SerializeField] private bool _landing = false;
    [SerializeField] private float _wallDownSpeed;


    private void Awake()
    {
        rig2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rig2d.velocity = new Vector2(_moveDirection.x * _currentSpeed, rig2d.velocity.y);
        OnVelocityChanged?.Invoke(_currentSpeed);
        OnJumped?.Invoke(!_canJumping);
        OnWallLanding?.Invoke(_landing);
    }

    public void Movement(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0)
        {
            if (Vector2.Dot(_moveDirection, direction) < 0)
            {
                _currentSpeed = 0;
            }
            _moveDirection = direction;
        }
        _currentSpeed = CalculateSpeed(direction, true);
    }

    private float CalculateSpeed(Vector2 direction, bool useDecreaseSpeed = false, bool useIncreaseSpeed = false)
    {
        if (direction.sqrMagnitude > 0)
        {
            if (useIncreaseSpeed)
            {
                _currentSpeed += _accelSpeed * Time.fixedDeltaTime * _AddSpeed;
            }
            else
            {
                _currentSpeed += _accelSpeed * Time.fixedDeltaTime;
            }
        }
        else
        {
            if (useDecreaseSpeed)
            {
                _currentSpeed -= _accelSpeed * Time.fixedDeltaTime * _AddSpeed;
            }
            else
            {
                _currentSpeed -= _accelSpeed * Time.fixedDeltaTime;
            }
        }

        return Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
    }

    public void Jump()
    {
        if (_canJumping || _landing)
        {
            rig2d.AddForce(new Vector2(0, _JumpPower), ForceMode2D.Impulse);
            _canJumping = false;
            _landing = false;
        }
    }

    public void CanNotMovement()
    {
        _accelSpeed = 0;
        _JumpPower = 0;
    }

    public void WallLanding(bool value)
    {
        if (value == true)
        {
            rig2d.MovePosition(new Vector2(rig2d.position.x, rig2d.position.y - (_wallDownSpeed * Time.fixedDeltaTime)));
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.CompareTag("Floor"))
        {
            _canJumping = true;
            _JumpPower = 35f;
        }
        if (col.collider.gameObject.CompareTag("Wall"))
        {
            _landing = true;
            _JumpPower = 20f;
        }
        else
        {
            _landing = false;
        }
    }
}



