using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour {

    public enum projectileTypes
    {
        TYPE_NONE = 0,
        TYPE_EXPLOSIVE = 1
    }

    public projectileTypes types = projectileTypes.TYPE_NONE;

    public float damage = 1f;
    public float bulletSpeed = 1.5f;
    public float lifeTime = 3f;

    public float wallDetectMargin = 0.9f;
    public float groundDetectMargin = 0.9f;

    public bool instantDeath = false;

    public float moveX;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(new Vector2(moveX, 0) * bulletSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D _other)
    {
        print("k");
        if (types == projectileTypes.TYPE_NONE)
        {
            print("l");
            if (_other.gameObject.tag == Constants.PLAYERTAG)
            {
                print("kk");
                Destroy(gameObject);
                _other.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            }
            else if (_other.gameObject.tag == Constants.ENEMYTAG)
            {
                print("ll");
                Destroy(gameObject);
                _other.gameObject.GetComponent<NPCHealth>().ChangeHealth(-damage);
            }
        }
        else if (types == projectileTypes.TYPE_EXPLOSIVE)
        {
            float lookDir = transform.localScale.x;
            lookDir /= Mathf.Abs(lookDir);

            foreach(ContactPoint2D contact in _other.contacts)
            {
                Vector2 pos = contact.normal;
                pos.x *= lookDir;

                if(pos.y > groundDetectMargin || pos.x < -wallDetectMargin || pos.x > wallDetectMargin)
                {
                    gameObject.GetComponent<Animator>(); // set trigger to explode;
                }
            }

            if(gameObject.GetComponent<Animator>()) // get trigger to explode
            {
                if (_other.gameObject.tag == Constants.PLAYERTAG && gameObject.tag != Constants.PLAYERTAG)
                {
                    Destroy(gameObject);
                    _other.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
                }
                else if (_other.gameObject.tag == Constants.ENEMYTAG && gameObject.tag != Constants.ENEMYTAG)
                {
                    Destroy(gameObject);
                    _other.gameObject.GetComponent<NPCHealth>().ChangeHealth(-damage);
                }
            }
        }
    }
}
