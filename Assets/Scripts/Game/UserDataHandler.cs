using UnityEngine;
using System.Collections;

public class UserDataHandler : MonoBehaviour
{
    #region Vars

    public UserData data;
    public bool autoLoad = true;

    #endregion

    #region Methods

    void Awake()
    {
        DontDestroyOnLoad(this);

        // Load data
        if (autoLoad) data = UserData.Load();
        else UserData.loaded = data;

        // Load
        Application.LoadLevel("Menu");
    }

    void OnApplicationQuit()
    {
        data.Save();
    }

    #endregion
}
