using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    #region Properties

    public float Speed = 3f;

    #endregion

    #region Vars

    private Animator _animator;
    private Vector3 _velocity;

    #endregion

    #region Methods

    void Update()
    {
        if (_velocity != Vector3.zero)
        {
            // Anim
            //_animator.SetBool("Running", true);

            // Rotate to velo
            float xScale = transform.localScale.x;
            float xDir = _velocity.x / Mathf.Abs(_velocity.x);

            if (xDir != xScale / Mathf.Abs(xScale))
            {
                transform.localScale = new Vector3(xDir, 1, 1);
            }

            // Apply velo
            transform.position += _velocity * Time.deltaTime;
        }
    }

    #endregion

    #region Get & Set

    public float Direction
    {
        set
        {
            // Bound value
            value = Mathf.Min(Mathf.Max(value, -1), 1);

            // Apply velo
            _velocity.x = value * Speed;
        }
    }

    #endregion

}
