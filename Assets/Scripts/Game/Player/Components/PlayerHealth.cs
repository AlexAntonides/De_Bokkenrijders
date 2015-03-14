using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

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
            print("Dead");
        }
    }
}
