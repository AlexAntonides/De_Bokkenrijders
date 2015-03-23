using UnityEngine;
using System.Collections;

public class HybridBehaviour : EnemyBehaviour {

    private float _meleeSpeed;
    private float _rangeSpeed;
    private float _throwSpeed;

    private float _tMelee;
    private float _tRange;
    private float _tThrow;

    public override void GetSpeed()
    {
        if (gameObject.GetComponent<Weapon>() != null)
        {
            _meleeSpeed = gameObject.GetComponent<Weapon>().attackSpeed;
        }

        if (gameObject.GetComponent<Gun>() != null)
        {
            _rangeSpeed = gameObject.GetComponent<Gun>().reloadSpeed;
        }
         
        if (gameObject.GetComponent<ThrowableObject>() != null)
        {
            _throwSpeed = gameObject.GetComponent<ThrowableObject>().throwSpeed;
        }
    }

    public override void CheckPlayer()
    {
        if (target != null)
        {
            float _distance = Vector2.Distance(target.transform.position, transform.position);

            _tMelee += Time.deltaTime;
            _tRange += Time.deltaTime;
            _tThrow += Time.deltaTime;

            if (_distance < attackRange)
            {
                state = EnemyStates.ATTACK;
                animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, false);

                if (_tMelee > _meleeSpeed && _meleeSpeed != 0)
                {
                    animator.SetTrigger(Constants.ANIMATOR_PARAMETER_ATTACK);
                    gameObject.GetComponent<Weapon>().Attack();
                    _tMelee = 0;
                }
            }
            else if (_distance < shootRange)
            {
                if (_tRange > _rangeSpeed && _rangeSpeed != 0)
                {
                    StopWalking();
                    animator.SetTrigger(Constants.ANIMATOR_PARAMETER_SHOOT);
                    //gameObject.GetComponent<Gun>().Shoot();
                    _tRange = 0;
                }
                else
                {
                    state = EnemyStates.WALK;
                }

                if (_tThrow > _throwSpeed && _throwSpeed != 0)
                {
                    StopWalking();
                    animator.SetTrigger(Constants.ENEMY_ANIMATOR_PARAMETER_THROW);
                    //gameObject.GetComponent<ThrowableObject>().Throw();
                    _tThrow = 0;
                }
                else
                {
                    state = EnemyStates.WALK;
                }
            }
            else if (_distance > attackRange)
            {
                state = EnemyStates.WALK;
            }
        }
    }

    void StopWalking()
    {
        state = EnemyStates.ATTACK;
        animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, false);
    }
}
