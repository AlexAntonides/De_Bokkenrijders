using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    #region Vars

    public PlayerMovement movementScript;
    public PlayerJump jumpScript;

    public ThumbStick inputStick;
    public float stickMoveStart = 0.2f;
    public float stickJumpStart = 0.7f;
    public float stickJumpReset = 0.1f;

    private bool _canJump = false;

    #endregion

    #region UnityMessages

    public void Start()
    {
    }

    public void FixedUpdate()
    {
        float thumbXDir = inputStick.stickUnitDirection.x;
        float thumbYDir = inputStick.stickUnitDirection.y;

        // Horizontal movement
        if (Mathf.Abs(thumbXDir) > stickMoveStart)
        {
            movementScript.Direction = thumbXDir;
        }
        else if (movementScript.Direction != 0) movementScript.Direction = 0;

        // Check jump
        if (_canJump && thumbYDir > stickJumpStart)
        {
            _canJump = false;
            jumpScript.Jump();
        }

        // Toggle jump stick
        if (!_canJump && thumbYDir < stickJumpReset)
        {
            _canJump = true;
        }

    }

    #endregion

    #region Methods

    #endregion

}
