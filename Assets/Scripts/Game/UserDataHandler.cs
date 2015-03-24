using UnityEngine;
using System.Collections;

public class UserDataHandler : MonoBehaviour
{
    #region Vars

    public UserData data;

    #endregion

    #region Methods

    void Awake()
    {
        DontDestroyOnLoad(this);

        // Load data
        data = UserData.Load();

        // Load last loaded level
        Application.LoadLevel((int)data.currentLevel);
    }

    void OnApplicationQuit()
    {
        data.Save();
    }

    #endregion
}
