using UnityEngine;
using System.Collections;

public class LoadSceneOnTap : MonoBehaviour {

    /* You don't have to multi-touch the Menu-Buttons so we use Click Events */

    public bool testModus = false;
    public Levels testLevel;

    [SerializeField]
    private string _sceneName;

    [SerializeField]
    private bool _currentLevel = false;

    void Start()
    {
        /* Check if the scene exists. */
        if (_sceneName != null)
        {
            if (Application.CanStreamedLevelBeLoaded(_sceneName) == false)
            {
                Debug.LogWarning(gameObject.name + " scene doesn't exist.");
            }
        }
        else
        {
            Debug.LogWarning(gameObject.name + " has an empty scene in the editor.");
        }
    }

    public void OnClick()
    {
        if (_currentLevel == true)
        {
            UserData.loaded.LoadCurrentLevel();
        }
        else
        {
            Application.LoadLevel(_sceneName);
        }
    }
}
