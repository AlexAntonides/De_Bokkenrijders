using UnityEngine;
using System.Collections;

public class PhaseController : MonoBehaviour {

    public GameObject deadWorld;
    public GameObject aliveWorld;

    public static bool test = true;

    public enum GamePhases
    {
        PHASE_ALIVE = 0,
        PHASE_DEAD = 1
    }

    private GamePhases _phase = GamePhases.PHASE_ALIVE;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_phase == GamePhases.PHASE_ALIVE)
            {
                _phase = GamePhases.PHASE_DEAD;

                StartCoroutine(ChangePhase(false));
            }
            else if(_phase == GamePhases.PHASE_DEAD)
            {
                _phase = GamePhases.PHASE_ALIVE;

                StartCoroutine(ChangePhase(true));
            }
        }
    }

    IEnumerator ChangePhase(bool currentWorld)
    {
        Slowmotion.StartMotion();

        yield return new WaitForSeconds(1.5f);

        test = currentWorld;

        aliveWorld.SetActive(currentWorld);
        deadWorld.SetActive(!currentWorld);

        Slowmotion.StopMotion();
    }
}
