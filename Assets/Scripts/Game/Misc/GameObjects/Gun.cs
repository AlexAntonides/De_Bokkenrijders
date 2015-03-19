using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour {

    public GameObject bullet;

    public float damage;
    public float reloadSpeed;
    public float bulletSpawnTime = 0.2f;
    public float bulletsToShoot = 3;

    [SerializeField]
    private const string SHOOT = "Shoot";
    private Animator _controller;
    
    void Start()
    {
        _controller = gameObject.GetComponent<Animator>();
    }
    
    public void Shoot()
    {
        _controller.SetTrigger(SHOOT);
        Invoke("SpawnBullet", bulletSpawnTime);
    }

    void SpawnBullet()
    {
        for (int i = 0; i < bulletsToShoot; i++)
        {
            Instantiate(bullet, new Vector2(transform.position.x + (GetComponentInParent<Transform>().lossyScale.x * 2), transform.position.y), transform.rotation);
            bullet.GetComponent<Projectile>().damage = damage;
            bullet.GetComponent<Projectile>().moveX = GetComponentInParent<Transform>().lossyScale.x;
        }
    }
}
