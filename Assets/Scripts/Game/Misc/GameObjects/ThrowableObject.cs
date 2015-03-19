using UnityEngine;
using System.Collections;

public class ThrowableObject : MonoBehaviour {

    public GameObject gObject;

    public float damage;
    public float throwSpeed;

    public void Throw()
    {
        Instantiate(gObject, transform.position, transform.rotation);
        gObject.GetComponent<Projectile>().damage = damage;
        gObject.GetComponent<Projectile>().types = Projectile.projectileTypes.TYPE_EXPLOSIVE;
    }
}
