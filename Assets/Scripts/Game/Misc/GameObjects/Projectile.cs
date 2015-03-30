using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{

    #region Vars
    public enum projectileTypes
    {
        TYPE_NONE = 0,          // No special effects.
        TYPE_EXPLOSIVE = 1      // Explode on touch ground.
    }

    public projectileTypes types = projectileTypes.TYPE_NONE;

    public GameObject owner;    // GameObject who shot the projectile.

    [HideInInspector]
    public float damage = 1f;           // Damage of the bullet set by gun.
    public float bulletSpeed = 1.5f;    // Speed of the bullet.
    public float lifeTime = 3f;         // Lifetime of bullet.
    [HideInInspector]
    public float moveX;                 // Way the bullet moves.
    public float moveY = 0;                 // Way the bullet moves.

    public bool instantDeath = false;   // Set true of projectile instantly kills the gameobject.

    private float _wallDetectMargin = 0.9f;
    private float _groundDetectMargin = 0.9f;
    #endregion

    #region Methods
    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the projectile after x seconds.
    }

    void Update()
    {
        transform.Translate(new Vector2(moveX, moveY) * bulletSpeed * Time.deltaTime); // Move the Projectile.
    }

    void OnCollisionEnter2D(Collision2D _other)
    {
        print(_other.gameObject.name);

        /* If type is explosive check every contactpoint. If Gameobject touches the ground, do the explode animation */
        if (types == projectileTypes.TYPE_EXPLOSIVE) 
        {
            float lookDir = transform.localScale.x; 
            lookDir /= Mathf.Abs(lookDir); 

            foreach(ContactPoint2D contact in _other.contacts)
            {
                Vector2 pos = contact.normal;
                pos.x *= lookDir;

                if (pos.y > _groundDetectMargin || pos.x < -_wallDetectMargin || pos.x > _wallDetectMargin)
                {
                    gameObject.GetComponent<Animator>().SetTrigger(Constants.PROJECTILE_ANIMATOR_PARAMETER_EXPLODE);
                    Destroy(gameObject, 1f);
                }
            }
        }

        /* If the GameObject touches a player or an enemy, check once again if he doesn't hit himself. If that isn't so, deal damage to the target */

        if (_other.gameObject.tag == Constants.TAG_PLAYER || _other.gameObject.tag == Constants.TAG_ENEMY)
        {
            if (_other.gameObject.tag != owner.tag)
            {
                if (instantDeath == false)
                {
                    _other.gameObject.GetComponent<Health>().health = -damage;
                }
                else if (instantDeath == true)
                {
                    _other.gameObject.GetComponent<Health>().health = -damage * 10;
                }

                if (types == projectileTypes.TYPE_EXPLOSIVE)
                {
                    gameObject.GetComponent<Animator>().SetTrigger(Constants.PROJECTILE_ANIMATOR_PARAMETER_EXPLODE);
                    Destroy(gameObject, 1.5f);
                    moveX = 0;
                    moveY = 0;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    #endregion
}
