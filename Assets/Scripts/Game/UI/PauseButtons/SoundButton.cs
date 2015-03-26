using UnityEngine;
using System.Collections;

public class SoundButton : ClickButton
{
    public GameObject disableGameObject;

    public override void FixedUpdate() { } // Reset Function.

    public override void ButtonPressed()
    {
        disableGameObject.SetActive(!disableGameObject.active);
    }
}
