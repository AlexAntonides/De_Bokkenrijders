using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class EnemyBehaviour : MonoBehaviour
{

    #region Enums
    public enum EnemyPhases
    {
        OUTOFCOMBAT = 0,                // Is the enemy out of combat. 
        INCOMBAT = 1                    // Is the player in combat.
    }

    public enum EnemyStates
    {
        IDLE = 0,                       // Current state: Idle.
        WALK = 1,                       // Current state: Walking.
        ATTACK = 2                      // Current state: Attacking.
    }

    public enum EnemyAttackTypes
    {
        MELEE = 0,                      // Type : Melee.    (Sword).
        RANGED = 1,                     // Type : Ranged.   (Gun).
        THROW = 2,                      // Type : Throw.    (Dynamite).
        HYBRID = 3                      // Type : Hybrid.   (Everything).
    }

    [SerializeField]
    public EnemyPhases phase = EnemyPhases.OUTOFCOMBAT;
    [SerializeField]
    public EnemyStates state = EnemyStates.IDLE;
    [SerializeField]
    public EnemyAttackTypes attackType = EnemyAttackTypes.MELEE;
    #endregion

    #region Vars
    public float moveSpeed = 1f;
    private float _attackSpeed;
    public float attackRange = 2f;
    public float shootRange = 4f;
    public float alertRange = 8f;
    public float knockBackRate = 2f;

    private float _walkDirection = 0;

    private float _tDir = 0;
    private float _tAttack = 0;

    [SerializeField]
    private Vector2 _walkTime;

    private Animator _animator;

    public GameObject target = null;
    #endregion

    #region Methods
    void Start()
    {
        gameObject.GetComponentInChildren<CircleCollider2D>().radius = alertRange;

        _animator = gameObject.GetComponent<Animator>();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        _tAttack = _attackSpeed;

        GetSpeed();
    }

    void Update()
    {
        if (gameObject.GetComponent<Health>().health > 0)
        {
            UpdatePhases();
            UpdateStates();
            UpdateSprite();
            CheckPlayer();
        }
    }

    void UpdatePhases()
    {
        if (phase == EnemyPhases.INCOMBAT)
        {
            InCombat();
        }
        else if (phase == EnemyPhases.OUTOFCOMBAT)
        {
            OutOfCombat();
        }
    }

    void InCombat()
    {
        if (target != null)
        {
            if (transform.position.x - target.transform.position.x > 0.5)
            {
                _walkDirection = -1;
            }
            else if (transform.position.x - target.transform.position.x < -0.5)
            {
                _walkDirection = 1;
            }
        }
        else if (target == null)
        {
            phase = EnemyPhases.OUTOFCOMBAT;
        }
    }

    void OutOfCombat()
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
                state = EnemyStates.WALK;
            }
        }
    }

    void UpdateStates()
    {
        if (state == EnemyStates.IDLE)
        {
            _animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, false);
            _walkDirection = 0;
        }
        else if (state == EnemyStates.WALK && _walkDirection != 0)
        {
            if (_animator.GetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK) != null)
            {
                _animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, true);
                transform.position = new Vector2((transform.position.x + _walkDirection * moveSpeed * Time.deltaTime), transform.position.y);
            }
        }
        else if (state == EnemyStates.ATTACK && phase == EnemyPhases.INCOMBAT)
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

    public virtual void CheckPlayer()
    {
        if (target != null)
        {
            float _distance = Vector2.Distance(target.transform.position, transform.position);

            if (_distance < attackRange)
            {
                state = EnemyStates.ATTACK;
                _animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, false);

                if (attackType == EnemyAttackTypes.MELEE)
                {
                    _attackSpeed = GetComponent<Weapon>().attackSpeed;

                    if (_tAttack > _attackSpeed)
                    {
                        _animator.SetTrigger(Constants.ANIMATOR_PARAMETER_ATTACK);
                        gameObject.GetComponent<Weapon>().Attack();
                        _tAttack = 0;
                    }
                }
                else if (attackType == EnemyAttackTypes.RANGED)
                {
                    _attackSpeed = GetComponent<Gun>().reloadSpeed;

                    if (_tAttack > _attackSpeed)
                    {
                        _animator.SetTrigger(Constants.ANIMATOR_PARAMETER_SHOOT);
                        gameObject.GetComponent<Gun>().Shoot();
                        _tAttack = 0;
                    }
                }
                else if (attackType == EnemyAttackTypes.THROW)
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
                state = EnemyStates.WALK;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.gameObject.tag == Constants.TAG_PLAYER && gameObject.GetComponent<BoxCollider2D>().enabled == true)
        {
            _other.gameObject.GetComponent<Health>().health = -gameObject.GetComponent<Weapon>().damage;
        }
    }

    void SetStateIdle()
    {
        state = EnemyStates.IDLE;
    }
    #endregion

    #region Getters & Setters

    public Animator animator
    {
        get
        {
            return _animator;
        }
    }

    public virtual void GetSpeed()
    {
        // Override void.
    }

    #endregion
}
