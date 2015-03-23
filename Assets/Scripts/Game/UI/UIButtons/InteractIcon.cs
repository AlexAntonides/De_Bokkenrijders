using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class InteractIcon : ClickButton {

    public GameObject interactedNPC;

    private NPCBehaviour behaviour;
    
    void Start()
    {
        behaviour = interactedNPC.GetComponent<NPCBehaviour>();
    }

    public override void ButtonPressed()
    {
        behaviour.interacted = !behaviour.interacted;
    }
}
