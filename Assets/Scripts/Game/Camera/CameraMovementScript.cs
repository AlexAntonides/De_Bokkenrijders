using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour
{
    #region Vars

    public float accelleration = 1f;
    public float maxSpeed = 6f;
    public float breakSpeed = 0.5f;
    public Vector3 minPos = Vector3.zero;
    public Vector3 maxPos = Vector3.zero;
    public GameObject followingObject;

    #endregion

    #region Methods

    void Update()
    {
        // Target point to look at
        float horizontalTargetX = followingObject.transform.position.x + followingObject.transform.localScale.x * Camera.main.orthographicSize / 4;
        float horizontalTargetY = followingObject.transform.position.y + followingObject.transform.localScale.y * Camera.main.orthographicSize / 4;

        // Calculate horizontal offset towards player look direction
        float horizontalOffsetX = horizontalTargetX - transform.position.x;
        float horizontalOffsetY = horizontalTargetY - transform.position.y;

        // Calc final pos
        Vector3 newPos = Vector3.Lerp(transform.position, transform.position + new Vector3(horizontalOffsetX, horizontalOffsetY, 0), maxSpeed * Time.deltaTime);

        // Bound pos
        if (minPos != Vector3.zero) newPos = Vector3.Max(newPos, minPos);
        if (maxPos != Vector3.zero) newPos = Vector3.Min(newPos, maxPos);

        // Move to target
        transform.position = newPos;

    }

    #endregion
}
