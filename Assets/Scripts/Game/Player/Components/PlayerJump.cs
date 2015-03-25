using UnityEngine;
using System.Collections.Generic;

public class PlayerJump : MonoBehaviour
{
    #region Vars

    // Physics
    public bool doubleJumpEnabled = true;
    public bool wallJumpEnabled = true;
    public float jumpForce = 500f;
    public float doubleJumpForce = 1000f;
    public float wallPushForce = 200f;

    // State
    private bool _jumping = false;
    private bool _doubleJump = false;
    private bool _wallJumping = false;

    // Other components
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private PlayerCollision _collision;
    private PlayerMovement _moveScript;
    
    #endregion

    #region Methods

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _moveScript = GetComponent<PlayerMovement>();
        _collision = GetComponent<PlayerCollision>();
    }

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<Health>().health > 0)
        {
            if (_jumping && _collision.GroundEntered)
            {
                _jumping = false;
                _doubleJump = false;
                _wallJumping = false;
                //_moveScript.enabled = true;
            }
            else if (_wallJumping && _collision.WallEntered)
            {
                //_moveScript.enabled = true;
                _wallJumping = false;
            }
            _jumping = false;
            _doubleJump = false;
            _wallJumping = false;
        }
        else if (_wallJumping && _collision.WallEntered)
        {
            _wallJumping = false;
        }
    }

    public void Jump()
    {
        // Normal Jump
        if (!_jumping && _collision.OnGround)
        {
            _jumping = true;
            _rigidbody.AddForce(jumpForce * Vector2.up);
            _animator.SetTrigger("Jump");
        }
        else if (_jumping && !_collision.OnGround)
        {
            // Wall Jump
            if (wallJumpEnabled && (_collision.OnLeftWall || _collision.OnRightWall))
            {
                _wallJumping = true;

                // Push back from wall
                _rigidbody.AddForce(new Vector2((_collision.OnLeftWall ? 1 : -1) * wallPushForce, 0));

                // Apply jump
                _rigidbody.AddForce(jumpForce * Vector2.up);

                _animator.SetTrigger("WallJump");
            }
            // Double jump
            else if (!_doubleJump && doubleJumpEnabled)
            {
                _doubleJump = true;
                _rigidbody.AddForce(doubleJumpForce * Vector2.up);
                _animator.SetTrigger("DoubleJump");
            }
        }
    }

    #endregion

    #region Get & Set

    public bool IsJumping
    {
        get { return _jumping; }
    }

    public bool IsWallJumping
    {
        get { return _wallJumping; }
    }

    #endregion
}
