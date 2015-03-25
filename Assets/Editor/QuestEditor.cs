using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

[CanEditMultipleObjects]
public class QuestEditor : Editor {

    public override void OnInspectorGUI()
    {
        NPCQuest _target = (NPCQuest)target;

        _target.questID = EditorGUILayout.IntField("Quest ID:", _target.questID);

        EditorGUILayout.LabelField("Quest Description:");
        //_target.questDescription = EditorGUILayout.TextArea(_target.questDescription, GUILayout.Height(75));

        EditorGUILayout.LabelField("Quest Method:");
        _target.method = (NPCQuest.questMethod)EditorGUILayout.EnumPopup(_target.method);

        switch (_target.method)
        {
            case NPCQuest.questMethod.METHOD_QUESTACCEPTABLE:
                ShowQuestOptions(_target);
                break;

            case NPCQuest.questMethod.METHOD_QUESTDISABLED:
                EditorGUILayout.LabelField("ID Quest to complete before he can start this quest:");
                _target.questIdCondition = EditorGUILayout.IntField(_target.questIdCondition);

                ShowQuestOptions(_target);
                break;
        }

        EditorGUILayout.LabelField("Quest Action On Complete:");
        _target.action = (NPCQuest.questAction)EditorGUILayout.EnumPopup(_target.action);

        switch (_target.action)
        {
            case NPCQuest.questAction.ACTION_CHANGE_SCENE:
                EditorGUILayout.LabelField("Scene Name:");
                _target.nameAction = EditorGUILayout.TextArea(_target.nameAction);
                break;
        }

        EditorGUILayout.LabelField("Amount of money rewarded:");
        _target.moneyReward = EditorGUILayout.FloatField(_target.moneyReward);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        Begin();
        _target.controller = (GameObject)EditorGUILayout.ObjectField("Controller:", _target.controller, typeof(GameObject), true);
        End();
        Begin();
        _target.questUI = (GameObject)EditorGUILayout.ObjectField("Quest UI:", _target.questUI, typeof(GameObject), true);
        End();
        Begin();
        _target.description = (Text)EditorGUILayout.ObjectField("Description:", _target.description, typeof(Text), true);
        End();
    }

    void Begin()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbarDropDown);
        EditorGUILayout.BeginVertical(EditorStyles.toolbarDropDown);
    }

    void End()
    {
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    void ShowQuestOptions(NPCQuest _target)
    {
        EditorGUILayout.LabelField("Complete Quest Message:");
        _target.questCompleteMessage = EditorGUILayout.TextArea(_target.questCompleteMessage);

        EditorGUILayout.LabelField("Quest Type:");
        _target.type = (NPCQuest.typeQuest)EditorGUILayout.EnumPopup(_target.type);

        switch (_target.type)
        {
            case NPCQuest.typeQuest.TYPE_KILL:
                EditorGUILayout.LabelField("Name of the creature who the player has to kill:");
                _target.nameObjective = EditorGUILayout.TextArea(_target.nameObjective);

                EditorGUILayout.LabelField("Amount of how many creatures he has to kill:");
                _target.amountEnemyToKill = EditorGUILayout.IntField(_target.amountEnemyToKill);
                break;

            case NPCQuest.typeQuest.TYPE_TALK:
                EditorGUILayout.LabelField("Name of NPC who the player has to talk:");
                _target.nameObjective = EditorGUILayout.TextArea(_target.nameObjective);
                break;
        }
    }

}
