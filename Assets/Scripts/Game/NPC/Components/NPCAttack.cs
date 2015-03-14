using UnityEngine;
using System.Collections;

public class NPCAttack : MonoBehaviour {

    public float damage = 1;

    public void Attack(GameObject _target)
    {
        _target.GetComponent<PlayerHealth>().ChangeHealth(-damage);
    }
}
