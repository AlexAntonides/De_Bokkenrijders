using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    private const string TAG_DEATH = "DeathObject";

    private PlayerCheckPoint _playerCheckPoint;

    void Start()
    {
        _playerCheckPoint = gameObject.GetComponent<PlayerCheckPoint>();
    }

    void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.transform.tag == TAG_DEATH)
        {
            transform.position = _playerCheckPoint.lastCheckPoint;
        }
    }
}
