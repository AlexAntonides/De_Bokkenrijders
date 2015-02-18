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
    private Animator _animator;

    #endregion

    #region Methods

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

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
            // Anim
            _animator.SetBool("Running", true);

            // Apply velo
            transform.position += _velo * Time.deltaTime;

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
            // Anim
            _animator.SetBool("Running", false);
        }
    }

    #endregion

}
