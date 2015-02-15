using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour
{
    #region Properties

    public float Accelleration = 1f;
    public float MaxSpeed = 6f;
    public float BreakSpeed = 0.5f;
    public GameObject FollowingObject;

    #endregion

    #region Vars

    private Vector3 _velo = Vector3.zero;
    
    #endregion

    #region Methods

    void Start()
    {
    }

    void Update()
    {
        // Target point to look at
        float horizontalTarget = FollowingObject.transform.position.x + FollowingObject.transform.localScale.x * Camera.main.orthographicSize / 4;
        float horizontalOffset = horizontalTarget - transform.position.x;

        // Move to target
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(horizontalOffset, 0, 0), MaxSpeed * Time.deltaTime);


        // Set step
        //_velo.x = horizontalOffset / Mathf.Abs(horizontalOffset) * MaxSpeed * Time.deltaTime;

        // Stop if too close
        //if (Mathf.Abs(horizontalOffset) < BreakSpeed)
        //{
        //    _velo.x = 0;
        //    transform.position = new Vector3(horizontalTarget, transform.position.y, transform.position.z);
        //}

        //if (_velo != Vector3.zero)
        //{
        //    // Move to point
        //    transform.position += _velo;
        //}
        
        //// Horizontal distance
        //float dis = FollowingObject.transform.position.x - transform.position.x;

        //// Too far from target
        //if (Mathf.Abs(dis) >= 5f)
        //{
        //    // Normalize * Scale by force
        //    float step = dis / Mathf.Abs(dis);
        //    step *= Force;

        //    // Move to target
        //    _velo = new Vector3(step, 0, 0);
        //}

        //// Apply velocity
        //if (_velo != Vector3.zero)
        //{
        //    // Apply velocity
        //    transform.position += _velo * Time.deltaTime;

        //    // Decellerate
        //    _velo -= _velo.normalized * BreakSpeed;

        //    // Stop if too close
        //    if (_velo.magnitude < BreakSpeed)
        //    {
        //        _velo *= 0;
        //    }
        //}
    }

    #endregion
}
