using UnityEngine;
using System.Collections;

public class ChangeCameraPosition : MonoBehaviour {

    public CameraMovementScript camera;

    public Vector2 minPos;
    public Vector2 maxPos;
    public Vector2 position;
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
