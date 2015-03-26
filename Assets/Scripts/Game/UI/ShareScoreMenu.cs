using System;
using System.Collections;
using UnityEngine;

public class ShareScoreMenu : MonoBehaviour
{
    #region Vars

    public Texture2D background;
    public GUIStyle userNameTextBoxStyle;
    public GUIStyle sendButtonStyle;
    public GUIStyle cancelButtonStyle;
    public GUIStyle currentScoreStyle;
    public GUIStyle highestScoresStyle;

    public string postScoreTarget = "http://localhost/School/Year_2/BAP/Mythe_HighScore/postScore.php";
    public string getScoreTarget = "http://localhost/School/Year_2/BAP/Mythe_HighScore/getScores.php";

    private string _currentScoreText = "";
    private int _currentScore = 0;
    private string _userName = "";
    private string[][] _highestScores = null;
    private bool _sending = false;

    #endregion

    #region Methods

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // Get highestScores
        StartCoroutine(GetScores());
    }

    private void OnGUI()
    {
        if (_sending) return;

        // Draw BG
        GUI.DrawTexture(new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.8f, Screen.height * 0.8f), background, ScaleMode.ScaleToFit);

        // UserName textfield
        _userName = GUI.TextArea(new Rect(Screen.width * 0.67f, Screen.height * 0.47f, Screen.width * 0.2f, Screen.height * 0.05f), _userName, 12, userNameTextBoxStyle);

        // Current Score
        GUI.TextArea(new Rect(Screen.width * 0.67f, Screen.height * 0.26f, Screen.width * 0.2f, Screen.height * 0.05f), _currentScoreText, currentScoreStyle);

        // Highest scores
        if (_highestScores != null)
        {
            if (_highestScores.Length >= 1)
            {
                GUI.TextArea(new Rect(Screen.width * 0.15f, Screen.height * 0.28f, Screen.width * 0.2f, Screen.height * 0.1f), _highestScores[0][0], highestScoresStyle);
                GUI.TextArea(new Rect(Screen.width * 0.45f, Screen.height * 0.28f, Screen.width * 0.2f, Screen.height * 0.1f), _highestScores[0][1], highestScoresStyle);
            }
            if (_highestScores.Length >= 2)
            {
                GUI.TextArea(new Rect(Screen.width * 0.15f, Screen.height * 0.44f, Screen.width * 0.2f, Screen.height * 0.1f), _highestScores[1][0], highestScoresStyle);
                GUI.TextArea(new Rect(Screen.width * 0.45f, Screen.height * 0.44f, Screen.width * 0.2f, Screen.height * 0.1f), _highestScores[1][1], highestScoresStyle);
            }
            if (_highestScores.Length >= 3)
            {
                GUI.TextArea(new Rect(Screen.width * 0.15f, Screen.height * 0.55f, Screen.width * 0.2f, Screen.height * 0.1f), _highestScores[2][0], highestScoresStyle);
                GUI.TextArea(new Rect(Screen.width * 0.45f, Screen.height * 0.55f, Screen.width * 0.2f, Screen.height * 0.1f), _highestScores[2][1], highestScoresStyle);
            }
            if (_highestScores.Length >= 4)
            {
                GUI.TextArea(new Rect(Screen.width * 0.15f, Screen.height * 0.66f, Screen.width * 0.2f, Screen.height * 0.1f), _highestScores[3][0], highestScoresStyle);
                GUI.TextArea(new Rect(Screen.width * 0.45f, Screen.height * 0.66f, Screen.width * 0.2f, Screen.height * 0.1f), _highestScores[3][1], highestScoresStyle);
            }
            if (_highestScores.Length >= 5)
            {
                GUI.TextArea(new Rect(Screen.width * 0.15f, Screen.height * 0.77f, Screen.width * 0.2f, Screen.height * 0.1f), _highestScores[4][0], highestScoresStyle);
                GUI.TextArea(new Rect(Screen.width * 0.45f, Screen.height * 0.77f, Screen.width * 0.2f, Screen.height * 0.1f), _highestScores[4][1], highestScoresStyle);
            }
        }

        // Send Button
        if (GUI.Button(new Rect(Screen.width * 0.67f, Screen.height * 0.59f, Screen.width * 0.19f, Screen.height * 0.1f), "", sendButtonStyle) && !string.IsNullOrEmpty(_userName))
        {
            UserData.loaded.name = _userName;
            StartCoroutine(SendScore());
        }

        // Cancel Button

    }

    private IEnumerator SendScore()
    {
        if (_sending) yield break;
        _sending = true;

        // Wait for done render this frame
        yield return new WaitForEndOfFrame();

        // Make SS & parse
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        byte[] ssData = ss.EncodeToPNG();

        // Create form
        WWWForm form = new WWWForm();
        form.AddField("score", CurrentScore.ToString());
        form.AddField("name", _userName);
        form.AddBinaryData("screenShot", ssData);

        // Send data
        WWW request = new WWW(postScoreTarget, form);
        yield return request;

        // Check result
        if (request.error == null)
        {
            Debug.Log("Succeed to post: " + request.text);
        }
        else
        {
            Debug.Log("Connection_Failure: " + request.error);
        }

        _sending = false;

        //StartCoroutine(GetScores());
        UserData.loaded.currentLevel++;
        UserData.loaded.LoadCurrentLevel();
    }

    private IEnumerator GetScores()
    {
        if (_sending) yield break;
        _sending = true;

        // Send data
        WWW request = new WWW(getScoreTarget);
        yield return request;

        // Check result
        if (request.error == null)
        {
            // Handle data
            string[] rows = request.text.Split(',');

            if (rows.Length != 0)
            {
                int rowsAmount = rows.Length;
                _highestScores = new string[rowsAmount][];
                for (int i = 0; i < rowsAmount; i++)
                {
                    _highestScores[i] = rows[i].Split(' ');

                    // Convert to easy to see time
                    TimeSpan time = TimeSpan.FromSeconds(double.Parse(_highestScores[i][1]));
                    _highestScores[i][1] = string.Format("{2}:{1}:{0}", time.Seconds, time.Minutes, time.Hours);
                }
            }
        }
        else
        {
            Debug.Log("Connection_Failure: " + request.error);
        }

        _sending = false;
    }

    #endregion

    #region Properties

    public int CurrentScore
    {
        set
        {
            _currentScore = value;
            TimeSpan time = TimeSpan.FromSeconds(value);
            _currentScoreText = string.Format("{2}:{1}:{0}", time.Seconds, time.Minutes, time.Hours);
        }
        get
        {
            return _currentScore;
        }
    }

    #endregion
}
