﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    #region Vars
    public float actionTime = 2.5f; // Do action after x seconds

    [SerializeField]
    public float _health = 5;      // Health of gameObject.
    [SerializeField]
    private float _curHealth = 5;      // Health of gameObject.
    private bool _isDead = false;   // Fix for animation.
    #endregion

    #region Methods

    void Start()
    {
        _curHealth = _health;
    }

    void Update()
    {
        if (_curHealth < 0)
        {
            Respawn();
        }
    }

    #endregion

    #region Getters & Setters
    public float health
    {
        get
        {
            return _curHealth;
        }
        set
        {
            _curHealth = _curHealth + value;

            if (_curHealth <= 0 && _isDead == false)
            {
                _isDead = true;
                gameObject.GetComponent<Animator>().SetTrigger(Constants.ANIMATOR_PARAMETER_DEATH);

                if (gameObject.tag != Constants.TAG_PLAYER)
                {
                    UserData.loaded.money = UserData.loaded.money + gameObject.GetComponent<EnemyBehaviour>().amountMoneyReward;
                }
            }
        }
    }

    public void DisableWeapon()
    {
        transform.Find("Weapons").gameObject.SetActive(false);
    }

    public void Respawn()
    {
        if (transform.tag == Constants.TAG_PLAYER)
        {
            transform.position = gameObject.GetComponent<PlayerCheckPoint>().lastCheckPoint;
            transform.Find("Weapons").gameObject.SetActive(true);
            _curHealth = _health;
            _isDead = false;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #endregion
}
