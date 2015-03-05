using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    private const string TAG_DEATH = "DeathObject";

    void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.transform.tag == TAG_DEATH)
        {
            transform.position = new Vector3(-55.68f, -13.56f, 0f);
        }
    }
}
