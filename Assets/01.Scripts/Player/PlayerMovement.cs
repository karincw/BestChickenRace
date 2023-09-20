using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private enum MapType
    {
        NONE = -1,
        FLOOR = 1,
        WALL = 2
    }


    [Header("속도가 변할때")]
    public UnityEvent<float> OnVelocityChanged;
    public UnityEvent<bool> OnJumped;
    public UnityEvent<bool> OnWallLanding;

    private Collider2D _col;
    private Rigidbody2D _rig2d;

    private Vector2 _moveDirection;

    [Header("Movement")]
    [SerializeField] private float _currentSpeed = 0f;
    [SerializeField] private float _accelSpeed = 5f;
    [SerializeField] private float _maxSpeed = 4f;
    [SerializeField] private float _AddSpeed = 3f;

    [Header("Jump")]
    [SerializeField] private float _JumpPower = 0f;
    [SerializeField] private bool _canJumping = true;
    [SerializeField] private LayerMask mapLayer;

    [Header("WallJump")]
    [SerializeField] private bool _landing = false;
    [SerializeField] private float _wallDownSpeed;


    private void Awake()
    {
        _rig2d = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        _rig2d.velocity = new Vector2(_moveDirection.x * _currentSpeed, _rig2d.velocity.y);
        OnVelocityChanged?.Invoke(_currentSpeed);
        OnJumped?.Invoke(!_canJumping);
        OnWallLanding?.Invoke(_landing);
        Debug.Log(MapCheck());


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
            _rig2d.velocity = Vector2.zero;
            _rig2d.AddForce(new Vector2(0, _JumpPower), ForceMode2D.Impulse);
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
            _rig2d.MovePosition(new Vector2(_rig2d.position.x, _rig2d.position.y - (_wallDownSpeed * Time.fixedDeltaTime)));
        }
    }

    private MapType MapCheck()
    {
        RaycastHit2D floorRay = Physics2D.BoxCast(_col.bounds.center, _col.bounds.size, 0f, Vector3.down, 0.01f, mapLayer);
        if (floorRay.collider != null)
        {
            if (_landing == true)
                _landing = false;  
            _canJumping = true;
            return MapType.FLOOR;
        }
        RaycastHit2D rightWallRay = Physics2D.BoxCast(_col.bounds.center, _col.bounds.size, 0f, Vector3.right, 0.01f, mapLayer);
        RaycastHit2D leftWallRay = Physics2D.BoxCast(_col.bounds.center, _col.bounds.size, 0f, Vector3.left, 0.01f, mapLayer);
        if (rightWallRay.collider != null || leftWallRay.collider != null)
        {
            _landing = true;
            return MapType.WALL;
        }
        _landing = false;
        _canJumping = false;
        return MapType.NONE;
    }


}



