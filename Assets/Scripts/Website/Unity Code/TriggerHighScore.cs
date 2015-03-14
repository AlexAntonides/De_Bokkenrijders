using UnityEngine;
using System.Collections;

public class TriggerHighScore : MonoBehaviour {
    public static int highScore;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(SaveHighScore());
        }
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            highScore += 100;
            gameObject.transform.position = new Vector2(-10, -10);
        }
    }

    IEnumerator SaveHighScore()
    {
        string stringURL = "http://localhost/index/mythe/savescore.php?" + "score=" + highScore;

        WWW url = new WWW(stringURL);
        yield return url;

        if (url.error != null)
        {
            print("There was an error: " + url.error);
        }
    }
}
