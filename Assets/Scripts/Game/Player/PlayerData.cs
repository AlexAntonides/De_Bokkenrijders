using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {

    /* This is the Class that holds the current Data of the player such as: Gold, Ammunition etc. */

    private int _gold;
    private int _ammunition;

    public int gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
        }
    }

    public int ammunition
    {
        get
        {
            return _ammunition;
        }
        set
        {
            _ammunition = value;
        }
    }
}
