using UnityEngine;
using System.Collections;

public class MovingGameObject : MonoBehaviour
{
    #region Vars
    public Vector2 movePosition;            // Move position.
    public float velocity;                  // Velocity of moving Object.

    private Vector2 curPosition;            // Current moving position.
    [SerializeField]
    private bool stopOnCollision = true;    // Set by Editor: Should object stop when collides with a player.
    #endregion

    #region Methods
    void Start()
    {
        curPosition = movePosition; // Set current position to the position he should move to.
    }

    void Update()
    {
        transform.Translate(curPosition * velocity * Time.deltaTime); // Move the object.
    }

    void OnCollisionEnter2D(Collision2D _other)
    {
        /* If GameObject collides with player, prevent the GameObject from moving */

        if (stopOnCollision)
        {
            if (_other.gameObject.tag == Constants.TAG_PLAYER)
            {
                curPosition = new Vector2(0, 0);
            }
        }
    }

    void OnCollisionExit2D(Collision2D _other)
    {
        /* If player exits the GameObject, move the GameObject again. */

        if (stopOnCollision)
        {
            if (_other.gameObject.tag == Constants.TAG_PLAYER)
            {
                curPosition = movePosition;
            }
        }
    }

    #endregion
}
