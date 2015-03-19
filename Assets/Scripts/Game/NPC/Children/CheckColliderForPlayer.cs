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
        if (_other.tag == Constants.PLAYERTAG)
        {
            _enemyBehaviour.phase = EnemyBehaviour.EnemyPhases.PHASE_INCOMBAT;
            _enemyBehaviour.target = _other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == Constants.PLAYERTAG)
        {
            _enemyBehaviour.state = EnemyBehaviour.EnemyStates.STATE_IDLE;
            _enemyBehaviour.phase = EnemyBehaviour.EnemyPhases.PHASE_OUTOFCOMBAT;
            _enemyBehaviour.target = null;
        }
    }
}
