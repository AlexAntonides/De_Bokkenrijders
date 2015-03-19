using UnityEngine;
using System.Collections;

public class CurrentQuest : MonoBehaviour {
    public int questID;

    public string questName;
    public string questDescription;

    public NPCQuest.questMethod method;
    public NPCQuest.typeQuest type;
    public NPCQuest.questAction action;

    public string questCompleteMessage;

    public string nameObjective; // The name whom to talk / kill.
    public string nameAction; // The name of the scene to load.

    public int amountEnemyToKill;
    public float moneyReward;

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
