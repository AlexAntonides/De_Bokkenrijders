using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    #region Properties

    public ThumbStick InputStick;
    public float Speed = 3f;

    #endregion

    #region Vars

    private Vector3 _velo = Vector2.zero;

    #endregion

    #region Methods

    void Update()
    {
        // Set velo
        _velo.x = InputStick.StickUnitDirection.x * Speed;

        // Apply velo
        transform.position += _velo * Time.deltaTime;
    }

    #endregion

}
