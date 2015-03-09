using UnityEngine;
using System.Collections.Generic;

public class PlayerJump : MonoBehaviour
{
    #region Properties

    public float Force;
    
    #endregion

    #region Vars

    private bool _falling = false;
    private bool _jumping = false;
    private bool _onGround = false;
    private List<GameObject> _connectedGrounds;

    #endregion

    #region Methods

    void Start()
    {
        _connectedGrounds = new List<GameObject>();
    }

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
                    _connectedGrounds.Add(other.gameObject);
                    Debug.Log("Ground connect");
                    break;
                }
                else if (Mathf.Abs(item.normal.x) >= 0.8f)
                {
                    Debug.Log("Wall hit");
                }
                else
                {
                    Debug.Log("Other collision: " + item.normal.ToString());
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
        if (_onGround)
        {
            foreach (GameObject current in _connectedGrounds)
            {
                if (other.gameObject == current)
                {
                    _connectedGrounds.Remove(current);

                    // Last ground connected
                    if (_connectedGrounds.Count == 0)
                    {
                        _onGround = false;
                        Debug.Log("Ground lost");
                    }

                    break;
                }
            }
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
