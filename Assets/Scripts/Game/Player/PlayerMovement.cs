using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    #region Properties

    public float Speed = 3f;

    #endregion

    #region Vars

<<<<<<< HEAD
    private Animator _animator;
    private Vector3 _velocity;
=======
    private Vector3 _velo = Vector3.zero;
>>>>>>> origin/master

    #endregion

    #region Methods

    void Update()
    {
        if (_velocity != Vector3.zero)
        {
<<<<<<< HEAD
            // Anim
            _animator.SetBool("Running", true);
=======
            // Apply velo
            transform.position += _velo * Time.deltaTime;
>>>>>>> origin/master

            // Rotate to velo

            float xScale = transform.localScale.x;
<<<<<<< HEAD
            float xDir = _velocity.x / Mathf.Abs(_velocity.x);
=======

            float xDir = _velo.x / Mathf.Abs(_velo.x);
>>>>>>> origin/master

            if (xDir != xScale / Mathf.Abs(xScale))
            {
                transform.localScale = new Vector3(xDir, 1, 1);
            }

            transform.position += _velocity * Time.deltaTime;
        }
        else
        {

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
