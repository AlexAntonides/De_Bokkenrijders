using UnityEngine;
using UnityEngine.UI;

public class MenuFade : MonoBehaviour
{
    #region Vars

    public float fadeTime = 3f;

    private Image _image;
    private bool _fading = false;
    private bool _fadeDir;

    #endregion

    #region Methods

    private void Start()
    {
        _image = GetComponent<Image>();
        FadeOut();
    }

    private void Update()
    {
        if (_fading)
        {
            // Check if finished
            if (_image.color.a == (_fadeDir ? 1f : 0f))
            {
                _fading = false;
                return;
            }

            // Calc step
            float step = Time.deltaTime / fadeTime * (_fadeDir ? 1 : -1);

            // Apply fade
            _image.color += new Color(0f, 0f, 0f, step);
        }
    }

    public void FadeIn()
    {
        _fadeDir = true;
        _fading = true;

    }

    public void FadeOut()
    {
        _fadeDir = false;
        _fading = true;

    }

    #endregion
}
