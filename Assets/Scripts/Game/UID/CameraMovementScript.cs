using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour
{
    #region Properties

    public float Force = 4f;
    public float BreakSpeed = 3f;
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
        // Horizontal distance
        float dis = FollowingObject.transform.position.x - transform.position.x;

        // Too far from target
        if (Mathf.Abs(dis) >= 5f)
        {
            // Normalize * Scale by force
            float step = dis / Mathf.Abs(dis);
            step *= Force;

            // Move to target
            _velo = new Vector3(step, 0, 0);
        }

        // Apply velocity
        if (_velo != Vector3.zero)
        {
            // Apply velocity
            transform.position += _velo * Time.deltaTime;

            // Decellerate
            _velo -= _velo.normalized * BreakSpeed;

            // Stop if too close
            if (_velo.magnitude < BreakSpeed)
            {
                _velo *= 0;
            }
        }
    }

    #endregion
}
