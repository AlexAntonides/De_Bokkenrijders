using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    #region Vars

    private const string TAG_DEATH = "DeathObject";
    private PlayerCheckPoint _playerCheckPoint;

    private bool _sliding = false;

    // If player has collision at the sides
    [SerializeField]
    private bool _onGround = false;
    [SerializeField]
    private bool _onLeftWall = false;
    [SerializeField]
    private bool _onRightWall = false;
    private bool _groundEntered = false;
    private bool _wallEntered = false;

    // Per frame collision checks
    private bool _groundCheck = false;
    private bool _leftWallCheck = false;
    private bool _rightWallCheck = false;

    // Collision detection
    public float wallDetectMargin = 0.9f;
    public float groundDetectMargin = 0.9f;

    // Other components
    private Animator _animator;
    private PlayerMovement _movescript;
    
    #endregion

    #region Methods

    void Start()
    {
        _playerCheckPoint = GetComponent<PlayerCheckPoint>();
        _animator = GetComponent<Animator>();
        _movescript = GetComponent<PlayerMovement>();
    }

    void OnCollisionEnter2D(Collision2D _other)
    {
        // Death
        if (_other.transform.tag == TAG_DEATH)
        {
            _animator.SetTrigger(Constants.ANIMATOR_PARAMETER_DEATH);
        }
    }

    public void ResetCheckPoint()
    {
        transform.position = _playerCheckPoint.lastCheckPoint;
    }

    private void FixedUpdate()
    {
        _groundEntered = false;
        _wallEntered = false;
        
        // Ground Enter
        if (!_onGround && _groundCheck)
        {
            _animator.SetBool("OnGround", true);
            _groundEntered = true;
        }
        // Ground Exit
        else if (_onGround && !_groundCheck)
        {
            _animator.SetBool("OnGround", false);
        }
        // Wall Enter
        if (!_onLeftWall && !_onRightWall && (_leftWallCheck || _rightWallCheck))
        {
            _wallEntered = true;
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

        // Slide
        if ((_onRightWall || _onLeftWall) && !_onGround && !_sliding)
        {
            _sliding = true;
            _animator.SetBool("OnWall", true);
            _animator.SetTrigger("WallSlide");
            _movescript.LookingRight = _onLeftWall;
        }
        else if (_sliding && (!OnWall || _onGround))
        {
            _sliding = false;
            _animator.SetBool("OnWall", false);
        }
    }

    // Scan for connected items
    private void OnCollisionStay2D(Collision2D other)
    {
        foreach (ContactPoint2D contact in other.contacts)
        {
            Vector2 pos = contact.normal;

            // Below player
            if (pos.y > groundDetectMargin)
            {
                _groundCheck = true;
            }
            // Left wall
            if (pos.x > wallDetectMargin)
            {
                _leftWallCheck = true;
            }
            // Right wall
            else if (pos.x < -wallDetectMargin)
            {
                _rightWallCheck = true;
            }
        }

    }

    #endregion

    #region Properties

    public bool Sliding
    {
        get { return _sliding; }
    }

    public bool OnGround
    {
        get { return _onGround; }
    }

    public bool GroundEntered
    {
        get { return _groundEntered; }
    }

    public bool WallEntered
    {
        get { return _wallEntered; }
    }

    public bool OnWall
    {
        get { return _onLeftWall || _onRightWall; }
    }

    public bool OnLeftWall
    {
        get { return _onLeftWall; }
    }

    public bool OnRightWall
    {
        get { return _onRightWall; }
    }

    #endregion
}
