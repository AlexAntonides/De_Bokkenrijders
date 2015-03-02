using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    #region Properties

    public ThumbStick Stick;
    public float ThumbStickStart = 0.7f;
    public float ThumbStickReset = 0.1f;

    public float Force = 5f;
    public float Gravity = 0.1f;

    #endregion

    #region Vars

    private Vector3 _velo = Vector3.zero;
    private bool _jumping = false;
    private bool _onGround = false;
    private bool _canJump = false;


    #endregion

    #region Methods

    void Update()
    {
        // Check jump
        if (_canJump && !_jumping && _onGround && Stick.StickUnitDirection.y > ThumbStickStart)
        {
            Jump();
        }

        // Toggle jump stick
        if (!_canJump && Stick.StickUnitDirection.y < ThumbStickReset)
        {
            _canJump = true;
        }

        // Apply gravity
        if (!_onGround) _velo.y -= Gravity;

        // Apply velo
        transform.position += _velo * Time.deltaTime;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!_onGround && other.gameObject.tag == "Ground")
        {
            Ground();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (_onGround && other.gameObject.tag == "Ground")
        {
            _onGround = false;
        }
    }

    void Jump()
    {
        _velo.y = Force;
        _jumping = true;
        _canJump = false;
    }

    void Ground()
    {
        _onGround = true;
        _jumping = false;
        _velo.y = 0;
    }

    #endregion
}
