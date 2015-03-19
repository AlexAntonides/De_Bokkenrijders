using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPCShop : MonoBehaviour {
    public GameObject[] listWeapons;
    private int _curWeapon = 0;

    [SerializeField]
    private Text _weaponName;
    [SerializeField]
    private GameObject _showingWeapon;
    [SerializeField]
    private GameObject _rangeMeter;
    [SerializeField]
    private GameObject _speedMeter;
    [SerializeField]
    private GameObject _damageMeter;
    [SerializeField]
    private Text _cost;
    [SerializeField]
    private GameObject _shop;

    public void Shop()
    {
        if(_shop.active == false)
        {
            _shop.SetActive(true);
            SetWeapon();
        }
        else if (_shop.active == true)
        {
            _shop.SetActive(false);
        }
    }

    void SetWeapon()
    {
        Weapon weaponComp = listWeapons[_curWeapon].GetComponent<Weapon>();

        _weaponName.text = weaponComp.name;
        _showingWeapon.GetComponent<Image>().sprite = listWeapons[_curWeapon].GetComponent<SpriteRenderer>().sprite;
        _rangeMeter.transform.localScale = new Vector2(weaponComp.range, _rangeMeter.transform.lossyScale.y);
        _speedMeter.transform.localScale = new Vector2(weaponComp.attackSpeed, _speedMeter.transform.lossyScale.y);
        _damageMeter.transform.localScale = new Vector2(weaponComp.damage, _damageMeter.transform.lossyScale.y);
        _cost.text = weaponComp.cost.ToString();
    }

    public void ChangeWeapon(int amount)
    {
        _curWeapon = _curWeapon + amount;

        if (_curWeapon < 0)
        {
            _curWeapon = listWeapons.Length;
        }
        else if (_curWeapon > listWeapons.Length)
        {
            _curWeapon = 0;
        }

        SetWeapon();
    }
}
