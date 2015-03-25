using UnityEngine;
using System.Collections;

public class ExecutionerEnd : MonoBehaviour {

	public void VillageSaved()
    {
        GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER).GetComponent<PlayerData>().villageTakenOver = false;
        Application.LoadLevel(Application.loadedLevel);
    }
}
