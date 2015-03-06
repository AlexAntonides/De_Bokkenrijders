using UnityEngine;
using System.Collections;

public class MusicButton : ClickButton
{
    public override void FixedUpdate() { } // Reset Function.

    public override void ButtonPressed()
    {
        print("TURN OFF MUSIC");
    }
}
