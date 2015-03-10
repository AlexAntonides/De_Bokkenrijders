using UnityEngine;
using System.Collections.Generic;

public class PlayerJump : MonoBehaviour
{
    #region Vars

    // Physics
    public float jumpForce = 500f;
    public float doubleJumpForce = 1000f;
    public float wallPushForce = 200f;

    // Collision detection
    public float wallDetectMargin = 0.9f;
    public float groundDetectMargin = 0.9f;
    
    // State
    [SerializeField]
    private bool _jumping = false;
    [SerializeField]
    private bool _doubleJump = false;

    // If player has collision at the sides
    [SerializeField]
    private bool _onGround = false;
    [SerializeField]
    private bool _onLeftWall = false;
    [SerializeField]
    private bool _onRightWall = false;
    
    // Per frame collision checks
    private bool _groundCheck = false;
    private bool _leftWallCheck = false;
    private bool _rightWallCheck = false;

    private Animator _animator;
    
    #endregion

    #region Methods

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnGroundEnter()
    {
        if (_jumping)
        {
            _jumping = false;
            _doubleJump = false;
        }

        _animator.SetBool("OnGround", true);
    }

    private void OnGroundExit()
    {
        _animator.SetBool("OnGround", false);
    }

    private void OnWallEnter(bool rightWall)
    {
        transform.localScale.Scale(new Vector3(-1, 1, 1));
    }

    private void OnWallExit(bool rightWall)
    {

    }

    private void FixedUpdate()
    {
        // Enter checks
        if (!_onGround && _groundCheck)
        {
            OnGroundEnter();
        }
        else if (_onGround && !_groundCheck)
        {
            OnGroundExit();
        }
        if (!_onLeftWall && _leftWallCheck)
        {
            OnWallEnter(false);
        }
        else if (_onLeftWall && !_leftWallCheck)
        {
            OnWallExit(false);
        }
        if (!_onRightWall && _rightWallCheck)
        {
            OnWallEnter(true);
        }
        else if (_onRightWall && !_rightWallCheck)
        {
            OnWallExit(true);
        }

        // Apply scan result
        _onGround = _groundCheck;
        _onLeftWall = _leftWallCheck;
        _onRightWall = _rightWallCheck;

        // Reset check for next scan
        _groundCheck = false;
        _leftWallCheck = false;
        _rightWallCheck = false;
        _leftWallCheck = false;
    }

    // Scan for connected items
    private void OnCollisionStay2D(Collision2D other)
    {
        // Get horizontal look direction
        float lookDir = transform.localScale.x;
        lookDir /= Mathf.Abs(lookDir);

        foreach (ContactPoint2D contact in other.contacts)
        {
            Vector2 pos = contact.normal;
            pos.x *= lookDir;

            //Debug.Log(lookDir + " : " + pos.ToString());

            // Check if collision in below player
            if (pos.y > groundDetectMargin)
            {
                _groundCheck = true;
            }
            // Left wall
            else if (pos.x < -wallDetectMargin)
            {
                _leftWallCheck = true;
            }
            // Right wall
            else if (pos.x > wallDetectMargin)
            {
                _rightWallCheck = true;
            }
        }
        
    }

    public void Jump()
    {
        // Normal Jump
        if (!_jumping && _onGround)
        {
            _jumping = true;
            // Apply jump
            GetComponent<Rigidbody2D>().AddForce(jumpForce * Vector2.up);

            _animator.SetTrigger("Jump");
        }
        else if (_jumping && !_onGround)
        {
            // Wall Jump
            if (_onLeftWall || _onRightWall)
            {
                // Push back from wall
                GetComponent<Rigidbody2D>().AddForce(new Vector2((_onLeftWall ? -1 : 1) * wallPushForce, 0));

                // Apply jump
                GetComponent<Rigidbody2D>().AddForce(jumpForce * Vector2.up);
            }
            // Double jump
            else if (!_doubleJump)
            {
                _doubleJump = true;
                // Apply jump
                GetComponent<Rigidbody2D>().AddForce(doubleJumpForce * Vector2.up);

                _animator.SetTrigger("DoubleJump");
            }
        }
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    // Look for new ground
    //    if (!_onGround)
    //    {
    //        // Check for collision on bottom
    //        foreach (ContactPoint2D item in other.contacts)
    //        {
    //            if (item.normal.y >= GroundDetectSensitivity)
    //            {
    //                _onGround = true;
    //                _connectedGrounds.Add(other.gameObject);
    //                Debug.Log("Ground connect");
    //                break;
    //            }
    //            else if (Mathf.Abs(item.normal.x) >= WallDetectSensitivity)
    //            {
    //                Debug.Log("Wall hit");
    //            }
    //            else
    //            {
    //                Debug.Log("Other collision: " + item.normal.ToString());
    //            }
    //        }

    //        if (_onGround && _jumping)
    //        {
    //            _jumping = false;
    //        }
    //    }
    //}

    //void OnCollisionExit2D(Collision2D other)
    //{
    //    if (_onGround)
    //    {
    //        foreach (GameObject current in _connectedGrounds)
    //        {
    //            if (other.gameObject == current)
    //            {
    //                _connectedGrounds.Remove(current);

    //                // Last ground connected
    //                if (_connectedGrounds.Count == 0)
    //                {
    //                    _onGround = false;
    //                    Debug.Log("Ground lost");
    //                }

    //                break;
    //            }
    //        }
    //    }
    //}

    #endregion

    #region Get & Set

    public bool IsJumping
    {
        get { return _jumping; }
    }

    public bool OnGround
    {
        get { return _onGround; }
    }

    public bool LeftWallCollision
    {
        get { return _onLeftWall; }
    }

    public bool RightWallCollision
    {
        get { return _onRightWall; }
    }

    #endregion
}
