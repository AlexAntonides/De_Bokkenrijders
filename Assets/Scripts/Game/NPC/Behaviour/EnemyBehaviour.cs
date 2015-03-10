using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyBehaviour : MonoBehaviour {

    [SerializeField]
    private bool enableAttack = false;

    public enum EnemyStates
    {
         STATE_OUTOFCOMBAT = 0,
         STATE_INCOMBAT = 1
    }

    private EnemyStates _state = EnemyStates.STATE_OUTOFCOMBAT;

    public float speed = 1f;
    public float attackRange = 5f;
    public float attackSpeed = 1.5f;

    private float attTimer;

    private GameObject player;

    private Vector2 moveRadPos;
    private Vector2 moveRadNeg;

    private Vector2 walkDirection;

    void Start()
    {
        if (gameObject.GetComponent<NPCAttack>() != null)
        {
            enableAttack = true;
        }

        moveRadPos = new Vector2(transform.position.x + GetComponent<CircleCollider2D>().radius, transform.position.y);
        moveRadNeg = new Vector2(transform.position.x - GetComponent<CircleCollider2D>().radius, transform.position.y); 
    }

    void Update()
    {
        if (_state == EnemyStates.STATE_OUTOFCOMBAT)
        {
            // Move Around();
        }
        else if (_state == EnemyStates.STATE_INCOMBAT)
        {
            if (player != null)
            {
                if (enableAttack == true) // If The Enemy has the ability to attack : Attack back.
                {
                    if (transform.position.x - player.transform.position.x > 0) // Left of him.
                    {
                        SetDirection(1);
                    }
                    else if (transform.position.x - player.transform.position.x < 0) // Right of him.
                    {
                        SetDirection(0);
                    }
                }
                else if (enableAttack == false) // If the Enemy doensn't have the ability to attack : Flee.
                {
                    if (transform.position.x - player.transform.position.x > 0) // Left of him.
                    {
                        SetDirection(0);
                    }
                    else if (transform.position.x - player.transform.position.x < 0) // Right of him.
                    {
                        SetDirection(1);
                    }
                }
            }
            else
            {
                _state = EnemyStates.STATE_OUTOFCOMBAT;
            }
        }

        Move();
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == Constants.PLAYERTAG)
        {
            _state = EnemyStates.STATE_INCOMBAT;
            player = _other.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D _other)
    {
        if (_other.tag == Constants.PLAYERTAG && player != null)
        {
            if (player.transform.position.x - transform.position.x <= attackRange && player.transform.position.x - transform.position.x > 0 || player.transform.position.x - transform.position.x >= -attackRange && player.transform.position.x - transform.position.x < 0)
            {
                attTimer += Time.deltaTime;

                if (attTimer > attackSpeed)
                {
                    gameObject.GetComponent<NPCAttack>().Attack(player);
                    attTimer = 0;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == Constants.PLAYERTAG)
        {
            _state = EnemyStates.STATE_OUTOFCOMBAT;
            SetDirection(2);
            player = null;
        }
    }

    void Move()
    {
        transform.Translate(walkDirection);
    }

    void SetDirection(int i)
    {
        switch (i)
        {
            case 0:
                walkDirection.x = 1 * speed * Time.deltaTime;
                break;
     
           case 1:
                walkDirection.x = -1 * speed * Time.deltaTime;
                break;

           case 2:
                walkDirection.x = 0 * speed * Time.deltaTime;
                break;
        }
    }
}
