using UnityEngine;
using System.Collections;

public class ThrowableObject : MonoBehaviour
{

    #region Vars
    public GameObject gObject;
    public Transform spawnPoint;

    public float damage;
    public float throwSpeed;

    public float moveY = -0.5f;
    #endregion

    #region Method
    public void Throw()
    {
        Instantiate(gObject, spawnPoint.position, Quaternion.Euler(new Vector2(spawnPoint.rotation.x, spawnPoint.rotation.y - 0.5f)));

        Projectile projectileComponent = gObject.GetComponent<Projectile>();
        projectileComponent.damage = damage;
        projectileComponent.owner = gameObject;
        projectileComponent.lifeTime = 5f;
        projectileComponent.bulletSpeed = 10f;
        projectileComponent.moveX = transform.localScale.x;
        projectileComponent.moveY = moveY;
    }
    #endregion
}
