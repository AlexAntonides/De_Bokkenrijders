using UnityEngine;
using System.Collections;

public class PlaceEnemies : MonoBehaviour {

    public GameObject[] enableEnemies;
    public GameObject[] disableEnemies;
    private PlayerData _data;

	void Start () {
        _data = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).GetComponent<PlayerData>();

        if (_data.villageTakenOver == false)
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
