using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    #region Properties

    public float Force = 5f;
    public float Gravity = 0.1f;

    #endregion

    #region Vars

    private bool _jumping = false;
    private bool _onGround = false;
<<<<<<< HEAD
    private Animator _animator;
    private Vector3 _velocity;
=======
    private bool _canJump = false;

>>>>>>> origin/master

    #endregion

    #region Methods

    void Update()
    {
        // Apply gravity
        if (!_onGround) _velocity.y -= Gravity * Time.deltaTime;

        if (_velocity != Vector3.zero) transform.position += _velocity * Time.deltaTime;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!_onGround && other.gameObject.tag == "Ground")
        {
            _onGround = true;
            _velocity.y = 0;

            if (_jumping)
            {
                _jumping = false;
                _animator.SetBool("Jumping", false);
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (_onGround && other.gameObject.tag == "Ground")
        {
            _onGround = false;
        }
    }

    public void Jump()
    {
        if (_jumping || !_onGround) return;

        _velocity.y = Force;
        _jumping = true;
<<<<<<< HEAD
        _animator.SetBool("Jumping", true);
    }

=======
        _canJump = false;
    }

    void Ground()
    {
        _onGround = true;
        _jumping = false;
        _velo.y = 0;
    }

>>>>>>> origin/master
    #endregion
}
