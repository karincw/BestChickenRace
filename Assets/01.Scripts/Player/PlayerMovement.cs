using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("�ӵ��� ���Ҷ�")]
    public UnityEvent<float> OnVelocityChanged;
    public UnityEvent OnJumped;

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

    private void Awake()
    {
        rig2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rig2d.velocity = new Vector2(_moveDirection.x * _currentSpeed, rig2d.velocity.y);
        OnVelocityChanged?.Invoke(_currentSpeed);
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

    public void StopImmediately()
    {
        _moveDirection = Vector2.zero;
        _currentSpeed = 0;
    }

    public void Jump()
    {
        if(_canJumping)
        {
            rig2d.AddForce(new Vector2(0, _JumpPower), ForceMode2D.Impulse);
            _canJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("InnerObject"))
        {
            _canJumping = true;
        }
    }
}



