using UnityEngine;
using System.Collections;

public class NPCHealth : MonoBehaviour {

    public float destroyTime = 2.5f;

    [SerializeField]
    private float _health = 5;
    private bool _dead = false;

    public float GetHealth()
    {
        return _health;
    }

    public void ChangeHealth(float amount)
    {
        _health = _health + amount;

        if (_health <= 0 && _dead == false)
        {
            gameObject.GetComponent<Animator>().SetTrigger(Constants.ENEMY_ANIMATOR_PARAMETER_DEATH);
            Destroy(gameObject, destroyTime);
            _dead = true;
        }
    }
}
