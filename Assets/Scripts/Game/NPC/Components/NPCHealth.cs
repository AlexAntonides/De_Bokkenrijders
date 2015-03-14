using UnityEngine;
using System.Collections;

public class NPCHealth : MonoBehaviour {

    [SerializeField]
    private float health = 5;

    public float GetHealth()
    {
        return health;
    }

    public void ChangeHealth(float amount)
    {
        health = health + amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
