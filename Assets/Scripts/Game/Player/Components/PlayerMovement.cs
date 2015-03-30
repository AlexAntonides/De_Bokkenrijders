using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Vars

    public float speed = 5f;
    public float acceleration = 0.1f;
    public float wallPushTime = 1f;

    private float _moveDir = 0f;
    private bool _lookingRight = true;
    
    private float _wallPushTime = 0f;
    private bool _canWallPush = false;
    private bool _wallPushing = false;

    // Other components
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private PlayerCollision _collider;
    private PlayerJump _jumpscript;
    
    #endregion

    #region Methods

    private void Start()
    {
        _collider = GetComponent<PlayerCollision>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _jumpscript = GetComponent<PlayerJump>();
    }

    private void Update()
    {
        if (_wallPushing)
        {
            _wallPushTime += Time.deltaTime;
            
            // Wall push end from timeout
            if (_wallPushTime >= wallPushTime)
            {
                _wallPushing = false;
                _canWallPush = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<Health>().health > 0)
        {
            // Horizontal movement
            if (_moveDir != 0 && !_jumpscript.IsWallJumping)
            {
                Vector2 currentVelo = _rigidbody.velocity;
                Vector2 targetVelo = new Vector2(speed * _moveDir, currentVelo.y);
                
                if (currentVelo != targetVelo)
                {
                    _rigidbody.velocity = Vector2.Lerp(currentVelo, targetVelo, acceleration);
                }
            }

            // Wall push end from wall lost
            if (_canWallPush && !_collider.OnWall)
            {
                _wallPushing = false;
                _canWallPush = false;
            }
            // Enable wall pushing when connecting with wall
            else if (!_wallPushing && _collider.WallEntered)
            {
                _canWallPush = true;
            }
        }
    }

    public void ApplyRotation()
    {
        // Check if art is already rotated towards direction
        Vector3 currentScale = transform.localScale;
        bool currentLookingRight = currentScale.x / Mathf.Abs(currentScale.x) > 0;

        if (currentLookingRight != _lookingRight)
        {
            //Debug.Log("Now looking " + (_lookingRight ? "right" : "left"));
            float dir = _lookingRight ? 1 : -1;
            transform.localScale = new Vector3(dir * Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }

    #endregion

    #region Properties

    public float Direction
    {
        get
        {
            return _moveDir;
        }
        set
        {
            // Bound value
            value = Mathf.Min(Mathf.Max(value, -1), 1);

            // Allow player to push into wall
            if (!_wallPushing && _canWallPush && !_collider.OnGround && (_collider.OnLeftWall ? value < 0 : value > 0) && _rigidbody.velocity.y <= 0f)
            {
                _wallPushTime = 0f;
                _wallPushing = true;
            }

            // Stop player from pushing into wall
            if (!_wallPushing && _collider.OnWall && !_collider.OnGround)
            {
                if (_collider.OnLeftWall) value = Mathf.Max(value, 0f);
                if (_collider.OnRightWall) value = Mathf.Min(value, 0f);
            }
            
            _moveDir = value;

            // Rotate
            if (value != 0f && !_wallPushing && !_jumpscript.IsWallJumping)
            {
                LookingRight = value > 0f;
            }

            // Set anim
            if (_moveDir == 0f) _animator.SetBool("IsRunning", false);
            else _animator.SetBool("IsRunning", true);
        }
    }

    #endregion

    #region Properties

    public bool LookingRight
    {
        get { return _lookingRight; }
        set
        {
            _lookingRight = value;
            ApplyRotation();
        }
    }
    public bool LookingLeft
    {
        get { return !_lookingRight; }
        set
        {
            _lookingRight = !value;
            ApplyRotation();
        }
    }

    #endregion
}
