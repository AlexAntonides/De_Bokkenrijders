using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    #region Properties

    public float Speed = 3f;

    #endregion

    #region Vars

    private Animator _animator;
    private float _dir = 0f;

    #endregion

    #region Methods

    void Update()
    {
        
    }

    #endregion

    #region Get & Set

    public float Direction
    {
        set
        {
            // Bound value
            value = Mathf.Min(Mathf.Max(value, -1), 1);

            // Rotate
            float newDir = value / Mathf.Abs(value);
            if (_dir / Mathf.Abs(_dir) != newDir)
            {
                transform.localScale = new Vector3(newDir, 1, 1);
            }

            // Apply velo
            rigidbody2D.velocity = new Vector2(value * Speed, rigidbody2D.velocity.y);
            _dir = value;
        }
        get
        {
            return _dir;
        }
    }

    #endregion

}
