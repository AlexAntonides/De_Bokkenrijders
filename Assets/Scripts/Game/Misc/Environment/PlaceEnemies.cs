using UnityEngine;
using System.Collections;

public class PlaceEnemies : MonoBehaviour {

    public GameObject[] enableEnemies;
    public GameObject[] disableEnemies;

	void Start () {
        if (UserData.loaded.cityCleared == false)
        {
            for (int i = 0; i < enableEnemies.Length; i++)
            {
                SetActiveAll(enableEnemies[i], true);
            }
            
            for (int j = 0; j < disableEnemies.Length; j++)
            {
                SetActiveAll(disableEnemies[j], false);
            }
        }
        else
        {
            for (int i = 0; i < enableEnemies.Length; i++)
            {
                SetActiveAll(enableEnemies[i], false);
            }

            for (int j = 0; j < disableEnemies.Length; j++)
            {
                SetActiveAll(disableEnemies[j], true);
            }
        }
	}

    void SetActiveAll(GameObject npc, bool boolean)
    {
        npc.SetActive(boolean);
    }
}
