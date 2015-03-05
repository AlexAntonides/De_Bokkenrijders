using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    #region Properties

    public float Force;
    
    #endregion

    #region Vars

    private bool _falling = false;
    private bool _jumping = false;
    private bool _onGround = false;
    private GameObject _ground;

    #endregion

    #region Methods

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)) Jump();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Look for new ground
        if (!_onGround)
        {
            // Check for collision on bottom
            foreach (ContactPoint2D item in other.contacts)
            {
                if (item.normal.y >= 0.8f)
                {
                    _onGround = true;
                    _ground = other.gameObject;
                    break;
                }
            }

            if (_onGround && _jumping)
            {
                _jumping = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (_onGround && other.gameObject == _ground)
        {
            _onGround = false;
        }
    }

    public void Jump()
    {
        if (_jumping || !_onGround) return;

        _jumping = true;
        rigidbody2D.AddForce(Force * Vector2.up);
    }

    #endregion
}
