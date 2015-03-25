using UnityEngine;
using System.Collections;

public class DestroyEverythingInside : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == Constants.TAG_PLAYER)
        {
            _other.transform.position = _other.GetComponent<PlayerCheckPoint>().lastCheckPoint;
        }
    }
}
