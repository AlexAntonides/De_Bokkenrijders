using UnityEngine;
using System.Collections;

public class SoundButton : ClickButton
{
    public override void FixedUpdate() { } // Reset Function.

    public override void ButtonPressed()
    {
        print("TURN OFF SOUND");
    }
}
