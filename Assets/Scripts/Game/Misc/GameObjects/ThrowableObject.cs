using UnityEngine;
using System.Collections;

public class ThrowableObject : MonoBehaviour
{

    #region Vars
    public GameObject gObject;
    public Transform spawnPoint;

    public float damage;
    public float throwSpeed;
    #endregion

    #region Method
    public void Throw()
    {
        Instantiate(gObject, spawnPoint.position, transform.rotation);

        Projectile projectileComponent = gObject.GetComponent<Projectile>();
        projectileComponent.damage = damage;
        projectileComponent.owner = gameObject;
        projectileComponent.lifeTime = 5f;
        projectileComponent.bulletSpeed = 3f;
        projectileComponent.moveX = transform.localScale.x;
    }
    #endregion
}
