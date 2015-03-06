using UnityEngine;
using System.Collections;

public class PauseButton : ClickButton
{
    [SerializeField]
    private GameObject PauseScreen;

    [SerializeField]
    private bool isPauseMenu = false;

    public override void FixedUpdate() { if (isPauseMenu == false) { base.FixedUpdate(); } } // Reset Function.

    public override void ButtonPressed()
    {
        if (PauseScreen.active == true)
        {
            PauseScreen.SetActive(false);
            Pause(false);
        } 
        else if (PauseScreen.active == false)
        {
            PauseScreen.SetActive(true);
            Pause(true);
        }
    }

    void Pause(bool pause = true)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
