using UnityEngine;

public class WallJumpTutorial : TutorialDisplayer
{
    #region Vars

    public PlayerJump playerJumpScript;

    #endregion

    #region Methods

    protected override void ShowTutorial()
    {
        base.ShowTutorial();
        playerJumpScript.wallJumpEnabled = true;
    }

    #endregion
}