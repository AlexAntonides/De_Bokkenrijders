using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Properties

    public float speed = 5f;
    public float acceleration = 0.1f;

    #endregion

    #region Vars

    private Animator _animator;
    private float _moveDir = 0f;
    private float _lookDir = 1f;

    #endregion

    #region Methods

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Horizontal movement
        if (_moveDir != 0)
        {
            Vector2 currentVelo = rigidbody2D.velocity;
            Vector2 targetVelo = new Vector2(speed * _moveDir, currentVelo.y);
            
            if (currentVelo != targetVelo)
            {
                rigidbody2D.velocity = Vector2.Lerp(currentVelo, targetVelo, acceleration);
            }
        }

    }

    private void ApplyRotation()
    {
        if (_moveDir == 0f) return;

        // Check if art is already rotated towards direction
        Vector3 currentScale = transform.localScale;
        float newLookDir = _moveDir / Mathf.Abs(_moveDir);

        if (_lookDir != newLookDir)
        {
            _lookDir = newLookDir;
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

            if (_moveDir == 0f) _animator.SetBool("IsRunning", false);
            else _animator.SetBool("IsRunning", true);

            ApplyRotation();
        }
    }

    #endregion

}
