using UnityEngine;
using System.Collections;

public class AttackButton : ClickButton
{
    private const string WEAPONS = "Weapons";

    private float _curAttackTimer;
    private float _curShootTimer;

    private Weapon _weapon;
    private Gun _gun;

    void Start()
    {
        _weapon = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).transform.Find(WEAPONS).gameObject.GetComponent<Weapon>();
        _gun = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).transform.Find(WEAPONS).gameObject.GetComponent<Gun>();

        _curAttackTimer = _weapon.attackSpeed;
        _curShootTimer = _gun.reloadSpeed;
    }

    void FixedUpdate()
    {
        _curAttackTimer += Time.deltaTime;
        _curShootTimer += Time.deltaTime;
    }

    public override void ButtonPressed()
    {
        if (GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).GetComponent<Animator>().GetBool(Constants.PLAYER_ANIMATOR_PARAMETER_ONGROUND))
        {
            if (_curAttackTimer >= _weapon.attackSpeed)
            {
                _weapon.Attack();
                _curAttackTimer = 0;
            }
        }
    }

    public override void ButtonHold()
    {
        if (GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).GetComponent<Animator>().GetBool(Constants.PLAYER_ANIMATOR_PARAMETER_ONGROUND))
        {
            if (_curShootTimer >= _gun.reloadSpeed)
            {
                _gun.Shoot();
                _curShootTimer = 0;
            }
        }
    }
}
