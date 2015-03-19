using UnityEngine;
using System.Collections;

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

    public enum EnemyTypes
    {
        TYPE_NORMAL = 1,
        TYPE_BOSS = 2
    }

    public enum EnemyAttackTypes
    {
        TYPE_MELEE = 0,
        TYPE_RANGED = 1,
        TYPE_THROW = 2
    }

    [SerializeField]
    public EnemyPhases phase = EnemyPhases.PHASE_OUTOFCOMBAT;
    [SerializeField]
    public EnemyStates state = EnemyStates.STATE_IDLE;
    [SerializeField]
    public EnemyTypes type = EnemyTypes.TYPE_NORMAL;
    [SerializeField]
    public EnemyAttackTypes attackType = EnemyAttackTypes.TYPE_MELEE;

    public float moveSpeed = 1f;
    private float _attackSpeed;
    public float attackRange = 2f;
    public float alertRange = 8f;
    public float knockBackRate = 2f;

    private float _walkDirection = 0;

    private float _tDir = 0;
    private float _tAttack = 0;

    [SerializeField]
    private Vector2 _walkTime;

    private Animator _animator;

    public GameObject target = null;

    void Start()
    {
        gameObject.GetComponentInChildren<CircleCollider2D>().radius = alertRange;

        _animator = gameObject.GetComponent<Animator>();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        _tAttack = _attackSpeed;
    }

    void Update()
    {
        if (gameObject.GetComponent<NPCHealth>().GetHealth() > 0)
        {
            UpdatePhases();
            UpdateStates();
            UpdateSprite();
            CheckPlayer();
        }
    }

    void UpdatePhases()
    {
        if (phase == EnemyPhases.PHASE_INCOMBAT)
        {
            if (target != null)
            {
                if(transform.position.x - target.transform.position.x > 0)
                {
                    _walkDirection = -1;
                }
                else if(transform.position.x - target.transform.position.x < 0)
                {
                    _walkDirection = 1;
                }
            } 
            else if (target == null)
            {
                phase = EnemyPhases.PHASE_OUTOFCOMBAT;
            }
        }
        else if (phase == EnemyPhases.PHASE_OUTOFCOMBAT)
        {
            if (Time.time >= _tDir)
            {
                _walkDirection = Random.Range(-1, 2);
                _tDir = Time.time + Random.Range(_walkTime.x, _walkTime.y);

                if (_walkDirection == 0)
                {
                    SetStateIdle();
                }
                else
                {
                    state = EnemyStates.STATE_WALK;
                }
            }
        }
    }

    void UpdateStates()
    {
        if (state == EnemyStates.STATE_IDLE)
        {
            _animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, false);
            _walkDirection = 0;
        }
        else if (state == EnemyStates.STATE_WALK && _walkDirection != 0)
        {
            _animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, true);
            transform.position = new Vector2((transform.position.x + _walkDirection * moveSpeed * Time.deltaTime), transform.position.y);
        }
        else if (state == EnemyStates.STATE_ATTACK && phase == EnemyPhases.PHASE_INCOMBAT)
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

    void CheckPlayer()
    {
        if (target != null)
        {
            float _distance = Vector2.Distance(target.transform.position, transform.position);

            if (_distance < attackRange)
            {
                print("NU");
                state = EnemyStates.STATE_ATTACK;
                _animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, false);

                if (attackType == EnemyAttackTypes.TYPE_MELEE)
                {
                    _attackSpeed = GetComponent<Weapon>().attackSpeed;

                    if (_tAttack > _attackSpeed)
                    {
                        _animator.SetTrigger(Constants.ENEMY_ANIMATOR_PARAMETER_ATTACK);
                        gameObject.GetComponent<Weapon>().Attack();
                        _tAttack = 0;
                    }
                }
                else if (attackType == EnemyAttackTypes.TYPE_RANGED)
                {
                    _attackSpeed = GetComponent<Gun>().reloadSpeed;

                    if (_tAttack > _attackSpeed)
                    {
                        _animator.SetTrigger(Constants.ENEMY_ANIMATOR_PARAMETER_SHOOT);
                        gameObject.GetComponent<Gun>().Shoot();
                        _tAttack = 0;
                    }
                }
                else if (attackType == EnemyAttackTypes.TYPE_THROW)
                {
                    _attackSpeed = GetComponent<ThrowableObject>().throwSpeed;

                    if (_tAttack > _attackSpeed)
                    {
                        _animator.SetTrigger(Constants.ENEMY_ANIMATOR_PARAMETER_THROW);
                        gameObject.GetComponent<ThrowableObject>().Throw();
                        _tAttack = 0;
                    }
                }
            }
            else if (_distance > attackRange)
            {
                state = EnemyStates.STATE_WALK;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.gameObject.tag == Constants.PLAYERTAG && gameObject.GetComponent<BoxCollider2D>().enabled == true)
        {
            _other.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-gameObject.GetComponent<Weapon>().damage);
        }
    }

    void SetStateIdle()
    {
        state = EnemyStates.STATE_IDLE;
    }
}
