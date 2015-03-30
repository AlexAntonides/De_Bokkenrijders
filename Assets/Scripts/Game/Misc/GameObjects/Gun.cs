using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{

    #region Vars
    public GameObject bullet;                   // Bullet to shoot.
    public Transform spawnPoint;                // Bullet Spawnpoint.
     
    public float damage;                        // Damage that the gun does.
    public float reloadSpeed;                   // Reload speed.
    public float bulletSpawnTime = 0.2f;        // Fix for gun. Amount of seconds when the bullet spawns. 
    public float bulletsToShoot = 3;            // Amount of Bullets that the gun will shoot.

    private const string WEAPONS = "Weapons";

    private Animator _controller;
    private GameObject owner;                   
    #endregion

    #region Methods
    void Start()
    {
        /* Get all information needed on start */

        _controller = gameObject.GetComponent<Animator>();

        if (transform.parent != null)
        {
            owner = transform.parent.gameObject;
        }
        else
        {
            owner = gameObject;
        }
    }
    
    public void Shoot()
    {
        /* Play the Animation and spawn the bullet after x seconds */
        Invoke("SpawnBullet", bulletSpawnTime);
    }

    void SpawnBullet()
    {
        /* Get the Scale of the object. */

        float scaleX;

        if (transform.parent != null)
        {
            scaleX = gameObject.GetComponentInParent<Transform>().localScale.x;
            print(scaleX);
        }
        else
        {
            scaleX = transform.localScale.x;
        }

        /* Spawn x amount of bullets and set the information */

        for (int i = 0; i < bulletsToShoot; i++)
        {
            Instantiate(bullet, spawnPoint.position, transform.rotation);

            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.damage = damage; // Damage.
            projectile.moveX = scaleX;  // Scale of the object.
            projectile.bulletSpeed = 10f; // bulletspeed;
            projectile.owner = owner;   // Gameobject who shot the projectile.
            projectile.lifeTime = 10f;    // Lifetime of projectile.
        }
    }
    #endregion
}
