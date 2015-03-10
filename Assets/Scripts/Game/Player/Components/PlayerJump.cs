using UnityEngine;
using System.Collections.Generic;

public class PlayerJump : MonoBehaviour
{
    #region Properties

    public float JumpForce = 500f;
    public float WallPushForce = 200f;
    public float WallDetectMargin = 0.9f;
    public float GroundDetectMargin = 0.9f;
    
    #endregion

    #region Vars

    [SerializeField]
    private bool _jumping = false;
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
    
    #endregion

    #region Methods

    private void Start()
    {
    }

    private void OnGroundEnter()
    {
        if (_jumping)
        {
            _jumping = false;
            _doubleJump = false;
        }
    }

    private void OnGroundExit()
    {
    }

    private void FixedUpdate()
    {
        // Ground checks
        if (!_onGround && _groundCheck)
        {
            OnGroundEnter();
        }
        else if (_onGround && !_groundCheck)
        {
            OnGroundExit();
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
        foreach (ContactPoint2D contact in other.contacts)
        {
            // Check if collision in below player
            if (contact.normal.y > GroundDetectMargin)
            {
                _groundCheck = true;
            }
            // Left wall
            else if (contact.normal.x < -WallDetectMargin)
            {
                _leftWallCheck = true;
            }
            // Right wall
            else if (contact.normal.x > WallDetectMargin)
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
            rigidbody2D.AddForce(JumpForce * Vector2.up);
        }
        else if (_jumping && !_onGround)
        {
            // Wall Jump
            if (_onLeftWall || _onRightWall)
            {
                // Push back from wall
                rigidbody2D.AddForce(new Vector2((_onLeftWall ? -1 : 1) * WallPushForce, 0));

                // Apply jump
                rigidbody2D.AddForce(JumpForce * Vector2.up);
            }
            // Double jump
            else if (!_doubleJump)
            {
                _doubleJump = true;
                // Apply jump
                rigidbody2D.AddForce(JumpForce * Vector2.up);
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
