using UnityEngine;
using System.Collections;

public class TutorialDisplayer : MonoBehaviour
{
    #region Vars

    public Texture2D closeButtonImage;
    public GUIStyle closeButtonStyle;

    public Texture2D tutorialImage;
    public float tutorialSize = 0.8f;

    private bool _open = false;
    private bool _opened = false;

    private GameObject _connectedPlayer;

    #endregion

    #region Methods

    void Start()
    {
    }

    private void OnGUI()
    {
        if (_open)
        {
            float padding = (1f - tutorialSize) / 2f;

            // background
            GUI.DrawTexture(new Rect(Screen.width * padding, Screen.height * padding, Screen.width * tutorialSize, Screen.height * tutorialSize), tutorialImage, ScaleMode.ScaleToFit);

            // Close Button
            if (GUI.Button(new Rect(Screen.width * (padding + tutorialSize * 0.075f), Screen.height * (padding + tutorialSize * 0.24f), Screen.width * tutorialSize * 0.08f, Screen.width * tutorialSize * 0.08f), closeButtonImage, closeButtonStyle))
            {
                _open = false;
                //Camera.main.transform.FindChild("User Interface Design").gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != Constants.TAG_PLAYER || _opened || _open) return;

        _connectedPlayer = other.gameObject;
        ShowTutorial();
    }

    protected virtual void ShowTutorial()
    {
        _open = _opened = true;

        //Camera.main.transform.FindChild("User Interface Design").gameObject.SetActive(false);
    }

    #endregion
}
