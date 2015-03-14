using UnityEngine;
using System.Collections;

public class PhaseSwitchButton : ClickButton {

    public GameObject deadWorld;
    public GameObject aliveWorld;

    public enum GamePhases
    {
        PHASE_ALIVE = 0,
        PHASE_DEAD = 1
    }

    private GamePhases _phase = GamePhases.PHASE_ALIVE;

    public override void ButtonPressed()
    {
        if (_phase == GamePhases.PHASE_ALIVE)
        {
            _phase = GamePhases.PHASE_DEAD;

            aliveWorld.SetActive(false);
            deadWorld.SetActive(true);
         }
         else if (_phase == GamePhases.PHASE_DEAD)
         { 
             _phase = GamePhases.PHASE_ALIVE;

             aliveWorld.SetActive(true);
             deadWorld.SetActive(false);
         }

        GameObject.FindGameObjectWithTag(Constants.PLAYERTAG).transform.FindChild(Constants.EFFECTNAME).transform.FindChild(Constants.SWITCHEFFECTNAME).GetComponent<Animator>().SetTrigger("Switch");
    }
}
