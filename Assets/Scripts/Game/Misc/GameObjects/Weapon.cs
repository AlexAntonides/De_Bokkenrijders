using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public const string ATTACK = "Attack";

    public float damage;
    public float range;
    public float attackSpeed;
    public float cost;

    private Animator _controller;

    void Start()
    {
        _controller = gameObject.GetComponent<Animator>();
    }

    public void Attack()
    {
        _controller.SetTrigger(ATTACK);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DisableColliderAfterAttack()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.gameObject.tag != gameObject.tag && _other.gameObject.tag == Constants.ENEMYTAG)
        {
            _other.gameObject.GetComponent<NPCHealth>().ChangeHealth(-damage);
        }
    }
}
