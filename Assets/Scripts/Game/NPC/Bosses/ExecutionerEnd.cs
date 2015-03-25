using UnityEngine;
using System.Collections;

public class ExecutionerEnd : MonoBehaviour {

	public void VillageSaved()
    {
        UserData.loaded.cityCleared = false;
        Application.LoadLevel(Application.loadedLevel);
    }
}
