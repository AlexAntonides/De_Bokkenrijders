using UnityEngine;
using System.Collections;

public class AttackButton : HoldButton
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
        if (_weapon == null) _weapon = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).transform.Find(WEAPONS).gameObject.GetComponent<Weapon>();
        if (_gun == null) _gun = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).transform.Find(WEAPONS).gameObject.GetComponent<Gun>();

        if (_curAttackTimer < _weapon.attackSpeed)
            _curAttackTimer += Time.deltaTime;
        if (_curShootTimer < _gun.reloadSpeed)
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
        if (_curShootTimer >= _gun.reloadSpeed)
        {
            _weapon.GetComponent<Animator>().SetTrigger(Constants.ANIMATOR_PARAMETER_SHOOT);
            _curShootTimer = 0;
        }
    }
}
