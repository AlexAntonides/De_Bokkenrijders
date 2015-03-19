using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    public enum questAction
    {
        ACTION_NONE = 0,
        ACTION_CHANGE_SCENE = 1 // Change scene when finished.
    }

    public int questID;

    public string questName;
    public string questDescription;

    public questMethod method;
    public typeQuest type;
    public questAction action;

    public string questStartMessage;
    public string questCompleteMessage;

    public string nameObjective; // The name whom to talk / kill.
    public string nameAction; // The name of the scene to load.

    public int amountEnemyToKill;
    public int questIdCondition;

    public float moneyReward;

    [SerializeField]
    private GameObject _controller;
    [SerializeField]
    private GameObject _questUI;
    [SerializeField]
    private ThumbStick _inputStick; 
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _startText;
    [SerializeField] 
    private Text _description; 

    public bool show = false;
    private const string _questStartText = "Quest Start Text";
    private const string _questName = "Quest Name";
    
    void Update()
    {
        if (show == true)
        {
            _questUI.SetActive(true);
            _nameText.text = questName;
            _startText.text = questStartMessage;
            _description.text = questDescription;

            if (_inputStick.stickUnitDirection.y > 1)
            {
                AcceptQuest();
                show = false;
            }
        }
        else if (show == false)
        {
            _questUI.SetActive(false);
        }
    }

    public void Quest()
    {
        if (_controller.GetComponent<CurrentQuest>() == null)
        {
            if (!_controller.GetComponent<QuestsList>().finishedQuests.Contains(questID))
            {
                show = true;
            }
            else
            {
                print("You've already finished the quest.");
            }
        }
        else if (_controller.GetComponent<CurrentQuest>() != null)
        {
            print("There is already a quest, abandon it?");
        }
    }

    void AcceptQuest()
    {
        if (method != questMethod.METHOD_AUTOCOMPLETE)
        {
            CurrentQuest cQuest = _controller.GetComponent<CurrentQuest>();

            cQuest.questID = questID;
            cQuest.questName = questName;
            cQuest.questDescription = questDescription;
            cQuest.method = method;
            cQuest.type = type;
            cQuest.action = action;
            cQuest.questCompleteMessage = questCompleteMessage;
            cQuest.nameObjective = nameObjective;
            cQuest.nameAction = nameAction;
            cQuest.amountEnemyToKill = amountEnemyToKill;
            cQuest.moneyReward = moneyReward;
        }
        else
        {
            _controller.GetComponent<QuestsList>().finishedQuests.Add(questID);
            
            if (moneyReward > 0)
            {
                print("Give money.");
            }

            if (action == questAction.ACTION_CHANGE_SCENE && nameAction != null )
            {
                Application.LoadLevel(nameAction);
            }
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

    public Text nameText 
    {
        get
        {
            return _nameText;
        }
        set
        {
            _nameText = value;
        }
    }

    public Text startText
    {
        get
        {
            return _startText;
        }
        set
        {
            _startText = value;
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
