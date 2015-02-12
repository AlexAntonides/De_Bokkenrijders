using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    #region Properties

    public ThumbStick InputStick;
    public float Speed = 3f;
    public float jumpForce = 10f;

    #endregion

    #region Vars

    private Vector3 _velo = Vector2.zero;

    #endregion

    #region Methods

    void Update()
    {
        // Set velo
        _velo.x = InputStick.StickUnitDirection.x * Speed;

        if (InputStick.StickUnitDirection.y > 0)
        {
            _velo.y = InputStick.StickUnitDirection.y * jumpForce;
        } 

        // Apply velo
        transform.position += _velo * Time.deltaTime;
    }

    #endregion

}
