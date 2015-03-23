using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NPCBehaviour))]
[CanEditMultipleObjects]
public class NPCEditor : Editor {

    public override void OnInspectorGUI()
    {
        NPCBehaviour _target = (NPCBehaviour)target;

        EditorGUILayout.LabelField("Type of the NPC:");
        _target.npcType = (NPCBehaviour.NPCType)EditorGUILayout.EnumPopup(_target.npcType);
        
        EditorGUILayout.LabelField("Action of the NPC:");
        _target.npcActions = (NPCBehaviour.NPCActions)EditorGUILayout.EnumPopup(_target.npcActions);
        
        switch (_target.npcActions)
        {
            case NPCBehaviour.NPCActions.ACTION_MOVING:
                _target.movementSpeed = EditorGUILayout.FloatField("Movement Speed:", _target.movementSpeed);
                _target.walkTime = EditorGUILayout.Vector2Field("Walk Time:", _target.walkTime);
                break;
        }

        EditorGUILayout.LabelField("Kind of Interaction:");
        _target.npcInteraction = (NPCBehaviour.NPCInteraction)EditorGUILayout.EnumPopup(_target.npcInteraction);

        switch (_target.npcInteraction)
        {
            case NPCBehaviour.NPCInteraction.INTERACTION_NEARBY:
                _target.interactionRange = EditorGUILayout.FloatField("Interaction Range:", _target.interactionRange);
                break;
        }
    }
}
