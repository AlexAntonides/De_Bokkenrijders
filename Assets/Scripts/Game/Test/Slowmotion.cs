using UnityEngine;
using System.Collections;

public class Slowmotion : MonoBehaviour
{

    private static float _timeScale = 1;
    private static bool _start = false;

    void Update()
    {
        if (_start == false)
        {
            if (_timeScale >= Time.timeScale)
            {
                Time.timeScale += Time.deltaTime;
                Time.fixedDeltaTime += Time.deltaTime;
            }
        }
        else if (_start == true)
        {
            if (_timeScale < Time.timeScale)
            {
                Time.timeScale -= Time.deltaTime;
                Time.fixedDeltaTime -= Time.deltaTime;
            }
        }
    }

    public static void StartMotion()
    {
        _timeScale = 0.5f;
        _start = true;
    }

    public static void StopMotion()
    {
        _timeScale = 1;
        _start = false;
    }
}