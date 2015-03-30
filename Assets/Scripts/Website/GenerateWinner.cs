using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateWinner : MonoBehaviour {
    
    public Dictionary<int, string> _list;

    void Start()
    {
        int random = Random.Range(0, _list.Count - 1);
        print(_list[random]);
    }
}
