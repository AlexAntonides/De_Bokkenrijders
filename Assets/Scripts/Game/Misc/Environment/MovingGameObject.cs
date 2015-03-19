using UnityEngine;
using System.Collections;

public class MovingGameObject : MonoBehaviour {

    public Vector2 movePosition;
    public float velocity;
    public bool stopOnCollision = true;

    private Vector2 curPosition;

    void Start()
    {
        curPosition = movePosition;
    }

    void Update()
    {
        transform.Translate(curPosition * velocity * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D _other)
    {
        if (stopOnCollision)
        {
            if (_other.gameObject.tag == Constants.PLAYERTAG)
            {
                curPosition = new Vector2(0, 0);
            }
        }
    }

    void OnCollisionExit2D(Collision2D _other)
    {
        if (stopOnCollision)
        {
            if (_other.gameObject.tag == Constants.PLAYERTAG)
            {
                curPosition = movePosition;
            }
        }
    }
}
