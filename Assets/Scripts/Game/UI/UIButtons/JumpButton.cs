using UnityEngine;

class JumpButton : ClickButton
{
    #region Vars

    public PlayerJump playerJumpScript;

    #endregion

    #region UnityMessages

    #endregion

    #region Methods

    public override void ButtonPressed()
    {
        playerJumpScript.Jump();
    }

    public override void ButtonReleased()
    {
    }

    #endregion

}