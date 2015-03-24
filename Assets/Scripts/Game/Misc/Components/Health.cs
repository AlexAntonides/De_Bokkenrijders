using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    #region Vars
    public float actionTime = 2.5f; // Do action after x seconds

    [SerializeField]
    private float _health = 5;      // Health of gameObject.
    private float _curHealth = 5;      // Health of gameObject.
    private bool _isDead = false;   // Fix for animation.
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
                gameObject.GetComponent<Animator>().SetTrigger(Constants.ANIMATOR_PARAMETER_DEATH);
                _isDead = true;

                if (gameObject.tag != Constants.TAG_PLAYER)
                {
                    Destroy(gameObject, actionTime);
                }
                else
                {

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
        transform.Find("Weapons").gameObject.SetActive(true);
        transform.position = gameObject.GetComponent<PlayerCheckPoint>().lastCheckPoint;
        _curHealth = health;
        _isDead = false;
    }

    #endregion
}
