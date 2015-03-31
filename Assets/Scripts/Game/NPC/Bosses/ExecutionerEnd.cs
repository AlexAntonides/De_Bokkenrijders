using UnityEngine;
using System.Collections;

public class ExecutionerEnd : MonoBehaviour {

	public void VillageSaved()
    {
        UserData.loaded.cityCleared = true;
        UserData.loaded.LoadCurrentLevel();
    }
}
