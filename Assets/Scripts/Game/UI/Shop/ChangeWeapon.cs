using UnityEngine;
using System.Collections;

public class ChangeWeapon : ClickButton {

    public int actionID = 0; // Negative: Left. Positive: Right.
    public GameObject _currentShopkeeper;

    void Start()
    {
    }

    public void ButtonPressed()
    {
        _currentShopkeeper.GetComponent<NPCShop>().ChangeWeapon(actionID);
    }
}
