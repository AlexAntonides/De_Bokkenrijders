using UnityEngine;
using System.Collections;

public class CheatEngine : MonoBehaviour {

    public GameObject[] _locations;

    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Constants.PLAYERTAG);
    }

	void Update () {
	    if (Input.GetKeyDown(KeyCode.P))
        {
            int _randomNumber = Random.Range(0, _locations.Length);
            _player.transform.position = _locations[_randomNumber].transform.position;
        }
	}
}
