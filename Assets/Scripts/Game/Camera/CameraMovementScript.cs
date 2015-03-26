using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour
{
    #region Vars

    public float maxSpeed = 6f;
    public float rotationBreakSpeed = 5f;
    public Vector3 minPos = Vector3.zero;
    public Vector3 maxPos = Vector3.zero;
    public GameObject followingObject;

    #endregion

    #region Methods

    void Update()
    {
        // Target point to look at
        Vector3 targetPoint = followingObject.transform.position + followingObject.transform.localScale * Camera.main.orthographicSize / 4;
        //float horizontalTargetX = followingObject.transform.position.x + followingObject.transform.localScale.x * Camera.main.orthographicSize / 4;
        //float horizontalTargetY = followingObject.transform.position.y + followingObject.transform.localScale.y * Camera.main.orthographicSize / 4;
        
        // Bound position
        if (minPos != Vector3.zero) targetPoint = Vector3.Max(targetPoint, minPos);
        if (maxPos != Vector3.zero) targetPoint = Vector3.Min(targetPoint, maxPos);

        // Calculate offset
        Vector3 offset = targetPoint - transform.position;
        offset.z = 0;
        //float offsetX = horizontalTargetX - transform.position.x;
        //float offsetY = horizontalTargetY - transform.position.y;

        if (offset.magnitude > 0)
        {
            // Calc final pos
            Vector3 newPos = Vector3.Lerp(transform.position, transform.position + offset, maxSpeed * Time.deltaTime);

            // Move to target
            transform.position = newPos;
        }

        // Rotate back
        if (transform.eulerAngles.z != 0f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(transform.eulerAngles.z, 0f, rotationBreakSpeed * Time.deltaTime));
        }
    }

    public void Shake(float force)
    {
        Vector3 offsetForce = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * force;
        transform.position += offsetForce;
    }

    public void ShakeAngle(float angle)
    {
        transform.eulerAngles += new Vector3(0, 0, (Random.value < 0.5f ? -1 : 1) * angle);
    }

    #endregion
}
