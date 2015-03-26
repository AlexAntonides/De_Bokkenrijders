using UnityEngine;
using System.Collections;

public class PhaseSwitchButton : ClickButton {

    public GameObject deadWorld;
    public GameObject aliveWorld;

    public PlayAudio audioSource;
    public AudioClip audioClip;

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

        GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).transform.FindChild(Constants.NAME_EFFECT).transform.FindChild(Constants.NAME_SWITCHEFFECT).GetComponent<Animator>().SetTrigger("Switch");
        audioSource.PlayAudioFile(false, audioClip);
    }
}
