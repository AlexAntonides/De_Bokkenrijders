using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    #region Properties

    public PlayerMovement MovementScript;
    public PlayerJump JumpScript;

    public ThumbStick InputStick;
    public float StickMoveStart = 0.2f;
    public float StickJumpStart = 0.7f;
    public float StickJumpReset = 0.1f;

    #endregion

    #region Vars

    private bool _canJump = false;

    #endregion

    #region UnityMessages

    public void Start()
    {
    }

    public void Update()
    {
        float thumbXDir = InputStick.StickUnitDirection.x;
        float thumbYDir = InputStick.StickUnitDirection.y;

        // Horizontal movement
        if (Mathf.Abs(thumbXDir) > StickMoveStart)
        {
            MovementScript.Direction = thumbXDir;
        }
        else MovementScript.Direction = 0;

        // Check jump
        if (_canJump && thumbYDir > StickJumpStart)
        {
            _canJump = false;
            JumpScript.Jump();
        }

        // Toggle jump stick
        if (!_canJump && thumbYDir < StickJumpReset)
        {
            _canJump = true;
        }

    }

    #endregion

    #region Methods

    #endregion

}
