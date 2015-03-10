using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    #region Properties

    public float Speed = 5f;
    public float Acceleration = 0.1f;

    #endregion

    #region Vars

    private Animator _animator;
    private float _moveDir = 0f;

    #endregion

    #region Methods

    void FixedUpdate()
    {
        // Horizontal movement
        if (_moveDir != 0)
        {
            Vector2 currentVelo = rigidbody2D.velocity;
            Vector2 targetVelo = new Vector2(Speed * _moveDir, currentVelo.y);
            
            if (currentVelo != targetVelo)
            {
                rigidbody2D.velocity = Vector2.Lerp(currentVelo, targetVelo, Acceleration);
            }
        }

    }

    private void ApplyRotation()
    {
        if (_moveDir == 0f) return;

        // Check if art is already rotated towards direction
        Vector3 currentScale = transform.localScale;
        float currentLookDir = currentScale.x / Mathf.Abs(currentScale.x);
        float newLookDir = _moveDir / Mathf.Abs(_moveDir);

        if (currentLookDir != newLookDir)
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
            // Bound value
            value = Mathf.Min(Mathf.Max(value, -1), 1);
            
            _moveDir = value;

            ApplyRotation();
        }
    }

    #endregion

}
