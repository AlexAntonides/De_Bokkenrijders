using UnityEngine;
using System.Collections;

public class RestartButton : ClickButton
{
    public override void FixedUpdate() { } // Reset Function.

    public override void ButtonPressed()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
