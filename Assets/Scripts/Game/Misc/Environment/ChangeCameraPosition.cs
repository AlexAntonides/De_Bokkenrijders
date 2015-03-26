using UnityEngine;
using System.Collections;

public class ChangeCameraPosition : MonoBehaviour {

    public CameraMovementScript camera;

    public Vector3 minPos;
    public Vector3 maxPos;
    public Vector3 position;
    public float size;

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == Constants.TAG_PLAYER)
        {
            camera.minPos = minPos;
            camera.maxPos = maxPos;
            camera.gameObject.GetComponent<Camera>().orthographicSize = size;
            camera.transform.position = position;
        }
    }
}
