using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class CheckColliderForPlayer : MonoBehaviour {

    private EnemyBehaviour _enemyBehaviour;

    void Start()
    {
        _enemyBehaviour = GetComponentInParent<EnemyBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == Constants.TAG_PLAYER)
        {
            _enemyBehaviour.phase = EnemyBehaviour.EnemyPhases.INCOMBAT;
            _enemyBehaviour.target = _other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == Constants.TAG_PLAYER)
        {
            _enemyBehaviour.state = EnemyBehaviour.EnemyStates.IDLE;
            _enemyBehaviour.phase = EnemyBehaviour.EnemyPhases.OUTOFCOMBAT;
            _enemyBehaviour.target = null;
        }
    }
}
