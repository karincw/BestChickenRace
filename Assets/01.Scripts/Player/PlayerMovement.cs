using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("속도가 변할때")]
    public UnityEvent<float> OnVelocityChanged;

    private Rigidbody2D rig2d;

    private Vector2 _moveDirection;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _accelSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _desreaseSpeed;

    private void Awake()
    {
        rig2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rig2d.velocity = _moveDirection * _currentSpeed;
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
        _currentSpeed = CalculateSpeed(direction);
    }

    private float CalculateSpeed(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0)
        {
            _currentSpeed += _accelSpeed * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= _accelSpeed * Time.deltaTime * _desreaseSpeed;
        }

        return Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
    }

    public void StopImmediately()
    {
        _moveDirection = Vector2.zero;
        _currentSpeed = 0;
    }

}


