using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    private const string TAG_PORTAL = "Portal";

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == TAG_PORTAL)
        {
            if(gameObject.GetComponent<PlayerMovement>().InputStick.StickUnitDirection.y < 0)
            {
                EnterPortal(_other.gameObject);
            }
        }
    }

    void EnterPortal(GameObject _portal)
    {
        print("ENTER VILLAGE~");
    }
}
