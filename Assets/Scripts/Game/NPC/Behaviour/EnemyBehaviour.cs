using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class EnemyBehaviour : MonoBehaviour {

    public enum EnemyPhases
    {
        PHASE_OUTOFCOMBAT = 0,
        PHASE_INCOMBAT = 1
    }

    public enum EnemyStates
    {
        STATE_IDLE = 0,
        STATE_WALK = 1,
        STATE_ATTACK = 2
    }

    [SerializeField]
    private EnemyPhases _phase = EnemyPhases.PHASE_OUTOFCOMBAT;
    [SerializeField]
    private EnemyStates _state = EnemyStates.STATE_IDLE;

    public float moveSpeed = 1f;
    public float attackSpeed = 1.5f;
    public float attackRange = 2f;
    public float alertRange = 8f;

    private float _walkDirection = 0;

    private float _tDir = 0;
    private float _tAttack = 0;

    [SerializeField]
    private Vector2 _walkTime;

    private NPCAttack _attack;
    private Animator _animator;
    private BoxCollider2D _attackCollider;

    private GameObject _target = null;

    void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = alertRange;

        _attack = gameObject.GetComponent<NPCAttack>();
        _animator = gameObject.GetComponent<Animator>();
        _attackCollider = gameObject.GetComponent<BoxCollider2D>();

        _attackCollider.enabled = false;
        _tAttack = attackSpeed;
    }

    void Update()
    {
        UpdatePhases();
        UpdateStates();
        UpdateSprite();
    }

    void UpdatePhases()
    {
        if (_phase == EnemyPhases.PHASE_INCOMBAT)
        {
            if (_target != null)
            {
                if(transform.position.x - _target.transform.position.x > 0)
                {
                    _walkDirection = -1;
                }
                else if(transform.position.x - _target.transform.position.x < 0)
                {
                    _walkDirection = 1;
                }
            } 
            else if (_target == null)
            {
                _phase = EnemyPhases.PHASE_OUTOFCOMBAT;
            }
        }
        else if (_phase == EnemyPhases.PHASE_OUTOFCOMBAT)
        {
            if (Time.time >= _tDir)
            {
                _walkDirection = Random.Range(-1, 2);
                _tDir = Time.time + Random.Range(_walkTime.x, _walkTime.y);

                if (_walkDirection == 0)
                {
                    _state = EnemyStates.STATE_IDLE;
                }
                else
                {
                    _state = EnemyStates.STATE_WALK;
                }
            }
        }
    }

    void UpdateStates()
    {
        if (_state == EnemyStates.STATE_IDLE)
        {
            _animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, false);
            _walkDirection = 0;
        }
        else if (_state == EnemyStates.STATE_WALK && _walkDirection != 0)
        {
            _animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, true);
            transform.position = new Vector2((transform.position.x + _walkDirection * moveSpeed * Time.deltaTime), transform.position.y);
        }
        else if (_state == EnemyStates.STATE_ATTACK && _phase == EnemyPhases.PHASE_INCOMBAT)
        {
            _tAttack += Time.deltaTime;
        }
    }

    void UpdateSprite()
    {
        if (_walkDirection == 1)
        {
            transform.rotation = Quaternion.Euler(new Vector2(transform.rotation.x, 0));
        }
        else if (_walkDirection == -1)
        {
            transform.rotation = Quaternion.Euler(new Vector2(transform.rotation.x, 180));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector2(transform.rotation.x, transform.rotation.y));
        }
    }
    
    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == Constants.PLAYERTAG)
        {
            _phase = EnemyPhases.PHASE_INCOMBAT;
            _target = _other.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D _other)
    {
        if (_other.tag == Constants.PLAYERTAG)
        {
            float _distance = Vector2.Distance(_other.transform.position, transform.position);

            if (_distance < attackRange)
            {
                _state = EnemyStates.STATE_ATTACK;

                if (_tAttack > attackSpeed)
                {
                    _animator.SetTrigger(Constants.ENEMY_ANIMATOR_PARAMETER_ATTACK);
                    _attackCollider.enabled = true;
                    _tAttack = 0;
                }
            } 
            else if (_distance > attackRange)
            {
                _state = EnemyStates.STATE_WALK;
            }
        }
    }

    void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == Constants.PLAYERTAG)
        {
            _state = EnemyStates.STATE_IDLE;
            _phase = EnemyPhases.PHASE_OUTOFCOMBAT;
            _target = null;
        }
    }

    void OnCollisionEnter2D(Collision2D _other)
    {
        if(_other.gameObject.tag == Constants.PLAYERTAG && _attackCollider.enabled == true)
        {
            _attack.Attack(_other.gameObject);
        }
    }

    void DisableColliderAfterAttack()
    {
        _state = EnemyStates.STATE_IDLE;
        _attackCollider.enabled = false;
    }
}
