    using UnityEngine;
using System.Collections;

public class TakeableGameObject : MonoBehaviour {

    public enum Actions
    {
        GOLD = 0,
        SCORE = 1,
        REMOVE_GAMEOBJECT = 2
    }

    [SerializeField]
    private Actions _actions;

    public GameObject removeableGameObject;
    public int amount;

    void OnCollisionEnter2D(Collision2D _other)
    {
        Collision(_other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        Collision(_other.gameObject);
    }

    void Collision(GameObject _other)
    {
        if (_other.tag == Constants.TAG_PLAYER)
        {
            Destroy(gameObject);

            if (_actions == Actions.GOLD)
            {
                _other.GetComponent<PlayerData>().gold = amount;
            }
            else if (_actions == Actions.SCORE)
            {
                // Add score.
            }
            else if (_actions == Actions.REMOVE_GAMEOBJECT)
            {
                Destroy(removeableGameObject);
            }
        }
    }
}
