using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    #region Properties

    public float Speed = 3f;

    #endregion

    #region Vars

    private Animator _animator;
    private float _moveDir = 0f;
    private bool _canMove = true;

    #endregion

    #region Methods

    void Update()
    {
        
    }

    private void ApplyDirection()
    {
        if (!CanMove) return;

        ApplyRotation();

        // Apply velo
        rigidbody2D.velocity = new Vector2(_moveDir * Speed, rigidbody2D.velocity.y);
    }

    private void ApplyRotation()
    {
        if (_moveDir == 0f) return;

        // Check if art is already rotated towards direction
        float newLookDir = _moveDir / Mathf.Abs(_moveDir);
        Vector3 currentScale = transform.localScale;
        if (newLookDir != currentScale.x / Mathf.Abs(currentScale.x))
        {
            transform.localScale = new Vector3(newLookDir * Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }

    #endregion

    #region Get & Set

    public float Direction
    {
        get
        {
            return _moveDir;
        }
        set
        {
            if (value == _moveDir) return;

            // Bound value
            value = Mathf.Min(Mathf.Max(value, -1), 1);

            _moveDir = value;
            ApplyDirection();
        }
    }

    public bool CanMove
    {
        get
        {
            return _canMove;
        }
        set
        {
            _canMove = value;
            ApplyDirection();
        }
    }

    #endregion

}
