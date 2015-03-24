using UnityEngine;
using System.Collections;

public class PauseButton : ClickButton
{
    [SerializeField]
    private GameObject _pauseScreen;

    [SerializeField]
    private bool _isPauseMenu = false;

    [SerializeField]
    private GameObject _pauseButton;

    public override void FixedUpdate() { if (_isPauseMenu == false) { base.FixedUpdate(); } } // Reset Function.

    public override void ButtonPressed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.b, gameObject.GetComponent<SpriteRenderer>().color.g, 1);
            
        if (_pauseScreen.active == true)
        {
            Pause(false);
            _pauseScreen.SetActive(false);
            _pauseButton.SetActive(true);
        }
        else if (_pauseScreen.active == false)
        {
            Pause(true);
            _pauseScreen.SetActive(true);
            _pauseButton.SetActive(false);
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
