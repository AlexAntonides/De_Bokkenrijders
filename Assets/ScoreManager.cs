using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    // Singleton
    public static ScoreManager current;

    #region Vars

    public bool autoStart = true;
    public ShareScoreMenu displayMenu;

    [SerializeField]
    private bool _started = false;
    [SerializeField]
    private double _currentTime = 0f;

    #endregion

    #region Methods

    private void Start()
    {
        current = this;
        if (autoStart) BeginSession();
    }

    private void Update()
    {
        if (_started)
        {
            _currentTime += Time.deltaTime;
        }
    }

    public void BeginSession()
    {
        if (_started) return;
        _started = true;

        displayMenu.gameObject.SetActive(false);
        _currentTime = 0f;
    }

    public void EndSession()
    {
        if (!_started) return;
        _started = false;

        displayMenu.CurrentScore = (int)_currentTime;
        displayMenu.gameObject.SetActive(true);
    }

    #endregion

    #region Getters & Setters

    public double currentTime
    {
        get
        {
            return _currentTime;
        }
    }

    #endregion
}
