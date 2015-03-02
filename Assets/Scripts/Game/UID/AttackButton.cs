using UnityEngine;
using System.Collections;

public class AttackButton : ClickButton
{

    public override void ButtonPressed()
    {
        player.GetComponent<PlayerAttack>().Attack();
    }
}
