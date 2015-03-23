using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    #region Vars
    public float actionTime = 2.5f; // Do action after x seconds

    [SerializeField]
    private float _health = 5;      // Health of gameObject.
    private bool _isDead = false;   // Fix for animation.
    #endregion

    #region Getters & Setters
    public float health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = _health + value;

            if (_health <= 0 && _isDead == false)
            {
                gameObject.GetComponent<Animator>().SetTrigger(Constants.ANIMATOR_PARAMETER_DEATH);
                _isDead = true;

                if (gameObject.tag != Constants.TAG_PLAYER)
                {
                    Destroy(gameObject, actionTime);
                }
                else
                {
                    transform.Find("Weapons").gameObject.SetActive(false);
                }
            }
        }
    }
    #endregion
}
