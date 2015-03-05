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

            /* 1st prototype */

            GameObject.FindGameObjectWithTag("Player").renderer.material.color = Color.blue;

            /* 1st prototype.end */

            aliveWorld.SetActive(false);
            deadWorld.SetActive(true);
         }
         else if (_phase == GamePhases.PHASE_DEAD)
         { 
             _phase = GamePhases.PHASE_ALIVE;
             
             /* 1st prototype */

             GameObject.FindGameObjectWithTag("Player").renderer.material.color = Color.red;

             /* 1st prototype.end */

             aliveWorld.SetActive(true);
             deadWorld.SetActive(false);
         }
    }
}
