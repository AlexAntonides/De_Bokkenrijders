using UnityEngine;
using System.Collections;

public class PlayerCheckPoint : MonoBehaviour {
    public Vector2 lastCheckPoint;

    private Vector2 _startPosition;

    private const string CP_TAG = "Checkpoint";

    void Start()
    {
        _startPosition = transform.position;
        lastCheckPoint = _startPosition;
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.tag == CP_TAG)
        {
            lastCheckPoint = _other.transform.position;
            Destroy(_other.gameObject);
        }
    }
}
