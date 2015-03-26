using UnityEngine;
using System.Collections;

public class CurrentQuest : MonoBehaviour {
    public int questID;                     // ID of quest.

    public string questName;                // Name of quest.
    public string questDescription;         // Quest Description. (What the player has to do).

    public NPCQuest.questMethod method;     // Current Method.
    public NPCQuest.typeQuest type;         // Current Type.
    //public NPCQuest.questAction action;     // Current Action.

    public string questCompleteMessage;     // Message NPC gives when he completes the quest.

    public string nameObjective;            // The name whom to talk / kill.
    public string nameAction;               // The name of the scene to load.

    public int amountEnemyToKill;           // Amount of enemy the player has to kill.
    public float moneyReward;               // Money Reward.

    void Update()
    {
        if (type == NPCQuest.typeQuest.TYPE_TALK)
        {
            // Not made yet.
        }
        else if (type == NPCQuest.typeQuest.TYPE_KILL)
        {
            // Not made yet.
        }
    }
}
