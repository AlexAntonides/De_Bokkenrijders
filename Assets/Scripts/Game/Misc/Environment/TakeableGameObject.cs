    using UnityEngine;
using System.Collections;

public class TakeableGameObject : MonoBehaviour {

    public enum Actions
    {
        GOLD = 0,
        REMOVE_GAMEOBJECT = 1
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
                UserData.loaded.money = UserData.loaded.money + amount;
            }
            else if (_actions == Actions.REMOVE_GAMEOBJECT)
            {
                Destroy(removeableGameObject);
            }
        }
    }
}
