using UnityEngine;
using System.Collections;

public class Deadwall : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D _player)
    {
        if (_player.gameObject.tag == "Player")
        {
            _player.transform.position = new Vector3(-41.5f, -8.66f, 0f);
        }
    }
}
