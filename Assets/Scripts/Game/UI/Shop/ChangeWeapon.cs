using UnityEngine;
using System.Collections;

public class ChangeWeapon : ClickButton {

    public int actionID = 0; // Negative: Left. Positive: Right.
    public GameObject _currentShopkeeper;

    void Start()
    {
        print("LRELRESR");
    }

    public void ButtonPressed()
    {
        print("LOL");
        _currentShopkeeper.GetComponent<NPCShop>().ChangeWeapon(actionID);
    }
}
