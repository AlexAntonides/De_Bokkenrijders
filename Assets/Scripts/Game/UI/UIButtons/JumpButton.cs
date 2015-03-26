using UnityEngine;

class JumpButton : ClickButton
{
    #region Vars

    public PlayerJump playerJumpScript;
    public PlayAudio audioSource;

    #endregion

    #region UnityMessages

    #endregion

    #region Methods

    public override void ButtonPressed()
    {
        playerJumpScript.Jump();
        audioSource.PlayRandomJump();
    }

    public override void ButtonReleased()
    {
    }

    #endregion

}