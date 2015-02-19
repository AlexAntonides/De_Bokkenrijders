using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    #region Properties

    public ThumbStick InputStick;
    public float StickResponceStart = 0.2f;
    public float Speed = 3f;

    #endregion

    #region Vars

    private Vector3 _velo = Vector3.zero;

    #endregion

    #region Methods

    void Update()
    {
        // Set velo
        float thumbXDir = InputStick.StickUnitDirection.x;
        if (Mathf.Abs(thumbXDir) > StickResponceStart)
        {
            _velo.x = thumbXDir * Speed;
        }
        else _velo.x = 0;

        if (_velo != Vector3.zero)
        {
            // Apply velo
            transform.position += _velo * Time.deltaTime;

            // Change Animation
            gameObject.GetComponent<Animator>().SetTrigger("Moving");

            // Rotate to velo

            float xScale = transform.localScale.x;

            float xDir = _velo.x / Mathf.Abs(_velo.x);

            if (xDir != xScale / Mathf.Abs(xScale))
            {
                transform.localScale = new Vector3(xDir, 1, 1);
            }
        }
        else
        {
            gameObject.GetComponent<Animator>().ResetTrigger("Moving");
        }
    }

    #endregion

}
