using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
public class NPCBehaviour : MonoBehaviour {

    public enum NPCType // Unchangeable via script.
    {
        TYPE_VILLAGER = 0,
        TYPE_QUESTGIVER = 1,
        TYPE_TRADER = 2
    }

    public enum NPCActions // Changeable via script.
    {
        ACTION_IDLE = 0,
        ACTION_MOVING = 1
    }

    public enum NPCPhases // Changeable via script.
    {
        PHASE_ZERO = 0,
        PHASE_GIVEQUEST = 1,
        PHASE_TRADING = 2
    }

    public enum NPCInteraction // Unchangeable via script.
    {
        INTERACTION_NONE = 0,
        INTERACTION_NEARBY = 1,
        INTERACTION_BUTTON = 2
    }

    public NPCType npcType;
    public NPCActions npcActions;
    public NPCInteraction npcInteraction;
    public NPCPhases npcPhases = NPCPhases.PHASE_ZERO;

    private NPCActions _npcCurrentAction;

    public bool interacted = false;

    public float movementSpeed = 1f;
    public float interactionRange = 8f;

    private float _tDir = 0;
    private float _walkDirection = 0;

    public Vector2 walkTime;

    private Animator _animator;

    private GameObject _target = null;

    void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = interactionRange;
        _animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        UpdateActions();
        UpdatePhases();
    }

    void UpdateActions()
    {
        if (_npcCurrentAction == NPCActions.ACTION_IDLE)
        {
            _animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, false);
            _walkDirection = 0;
        }
        else if (_npcCurrentAction == NPCActions.ACTION_MOVING)
        {
            _animator.SetBool(Constants.ENEMY_ANIMATOR_PARAMETER_WALK, true);
            transform.position = new Vector2((transform.position.x + _walkDirection * movementSpeed * Time.deltaTime), transform.position.y);
        }
    }

    void UpdatePhases()
    {
        if (npcPhases == NPCPhases.PHASE_ZERO)
        {
            if (npcActions == NPCActions.ACTION_IDLE)
            {
                _npcCurrentAction = NPCActions.ACTION_IDLE;
            }
            else if (npcActions == NPCActions.ACTION_MOVING)
            {
                if (Time.time >= _tDir)
                {
                    _walkDirection = Random.Range(-1, 2);
                    _tDir = Time.time + Random.Range(walkTime.x, walkTime.y);

                    if (_walkDirection == 0)
                    {
                        _npcCurrentAction = NPCActions.ACTION_IDLE;
                    }
                    else
                    {
                        _npcCurrentAction = NPCActions.ACTION_MOVING;
                    }
                }
            }
        }
        else if (npcPhases == NPCPhases.PHASE_GIVEQUEST && _target != null)
        {
            _npcCurrentAction = NPCActions.ACTION_IDLE;
            // Open Quest Menu.
        }
        else if (npcPhases == NPCPhases.PHASE_TRADING && _target != null)
        {
            _npcCurrentAction = NPCActions.ACTION_IDLE;
            // Open Trade Menu.
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == Constants.TAG_PLAYER)
        {
            _target = _other.gameObject;

            if (npcInteraction == NPCInteraction.INTERACTION_NEARBY)
            {
                if (npcType == NPCType.TYPE_QUESTGIVER)
                {
                    npcPhases = NPCPhases.PHASE_GIVEQUEST;
                }
                else if (npcType == NPCType.TYPE_TRADER)
                {
                    npcPhases = NPCPhases.PHASE_TRADING;
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D _other)
    {
        if (_other.tag == Constants.TAG_PLAYER)
        {
            if (npcInteraction == NPCInteraction.INTERACTION_BUTTON && npcType != NPCType.TYPE_VILLAGER && interacted == true)
            {
                CheckTradeShop();
            }
            else if (npcInteraction == NPCInteraction.INTERACTION_NEARBY && npcType != NPCType.TYPE_VILLAGER)
            {
                CheckTradeShop();
            }
        }
    }

    void CheckTradeShop()
    {
        if (npcType == NPCType.TYPE_TRADER && npcPhases != NPCPhases.PHASE_TRADING && gameObject.GetComponent<NPCShop>() != null)
        {
            npcPhases = NPCPhases.PHASE_TRADING;
            gameObject.GetComponent<NPCShop>().Shop();
        }
        else if (npcType == NPCType.TYPE_QUESTGIVER && gameObject.GetComponent<NPCQuest>() != null)
        {
            npcPhases = NPCPhases.PHASE_GIVEQUEST;
            gameObject.GetComponent<NPCQuest>().Quest();
        }
    }

    void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == Constants.TAG_PLAYER)
        {
            _target = null;

            if (npcType != NPCType.TYPE_VILLAGER)
            {
                npcPhases = NPCPhases.PHASE_ZERO;

                if (npcType == NPCType.TYPE_QUESTGIVER)
                {
                    gameObject.GetComponent<NPCQuest>().show = false;
                }
                else if (npcType == NPCType.TYPE_TRADER)
                {
                    gameObject.GetComponent<NPCShop>().Shop();
                }
            }
        }
    }

}
