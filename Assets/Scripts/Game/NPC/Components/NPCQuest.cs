using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class NPCQuest : MonoBehaviour {

    public enum questMethod
    {
        METHOD_AUTOCOMPLETE = 0,        // Auto Complete The Quest After Dialogue.
        METHOD_QUESTACCEPTABLE = 1,     // Quest Acceptable.
        METHOD_QUESTDISABLED = 2        // Quest Disabled.
    }

    public enum typeQuest
    {
        TYPE_DIALOGUE = 0,  // Dialogue Quest.
        TYPE_TALK = 1,      // Talk To Other NPC Quest.
        TYPE_KILL = 2       // Kill amount of Creatures Quest.
    }

    //public enum questAction
    //{
    //    ACTION_NONE = 0,
    //    ACTION_CHANGE_SCENE = 1 // Change scene when finished.
    //}

    public int questID;

    public string questName;
    public string[] questDescription;

    public questMethod method;
    public typeQuest type;
    //public questAction action;

    public string questStartMessage;
    public string questCompleteMessage;

    public string nameObjective; // The name whom to talk / kill.
    //public string nameAction; // The name of the scene to load.

    public int amountEnemyToKill;
    public int questIdCondition;

    public float moneyReward;

    private int _curText = 0;

    [SerializeField]
    private GameObject _controller;
    [SerializeField]
    private GameObject _questUI;
    [SerializeField]
    private ThumbStick _inputStick; 
    [SerializeField] 
    private Text _description;
    [SerializeField]
    private Image _head;
    [SerializeField]
    private Sprite _headIMG;
    [SerializeField]
    private Image _name;
    [SerializeField]
    private Sprite _nameIMG;

    public bool show = false;
    private bool showOff = false;
    private const string _questStartText = "Quest Start Text";
    private const string _questName = "Quest Name";
    
    void Update()
    {
        if (show == true)
        {
            _questUI.SetActive(true);
            _head.sprite = _headIMG;
            _name.sprite = _nameIMG;
            _description.text = questDescription[_curText];
        }
        else if (show == false)
        {
            _questUI.SetActive(false);
        }
    }

    public void Quest()
    {
        if (_controller.GetComponent<CurrentQuest>() == null && showOff == false)
        {
            show = true;
        }
        else if (_controller.GetComponent<CurrentQuest>() != null)
        {
            print("There is already a quest, abandon it?");
        }
    }

    public void AcceptQuest()
    {
        if (_curText >= questDescription.Length - 1)
        {
            show = false;

            if (method != questMethod.METHOD_AUTOCOMPLETE)
            {
                /*
                CurrentQuest cQuest = _controller.GetComponent<CurrentQuest>();

                cQuest.questID = questID;
                cQuest.questName = questName;
                cQuest.questDescription = questDescription[_curText];
                cQuest.method = method;
                cQuest.type = type;
                cQuest.action = action;
                cQuest.questCompleteMessage = questCompleteMessage;
                cQuest.nameObjective = nameObjective;
                cQuest.nameAction = nameAction;
                cQuest.amountEnemyToKill = amountEnemyToKill;
                cQuest.moneyReward = moneyReward;
                 */
            }
            else
            {
                if (moneyReward > 0)
                {
                    print("Give money.");
                }

                //if (action == questAction.ACTION_CHANGE_SCENE && nameAction != null)
                //{
                //    Application.LoadLevel(nameAction);
                //}

                _questUI.SetActive(false);
                show = false;
                showOff = true;

                ScoreManager.current.EndSession();
            }
        }
        else
        {
            _curText++;
            _description.text = questDescription[_curText];
        }

    }

    public GameObject controller
    {
        get
        {
            return _controller;
        }
        set
        {
            _controller = value;
        }
    }

    public GameObject questUI
    {
        get
        {
            return _questUI;
        }
        set
        {
            _questUI = value;
        }
    }

    public ThumbStick inputStick
    {
        get
        {
            return _inputStick;
        }
        set
        {
            _inputStick = value;
        }
    }


    public Text description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }

}
