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
    private Slider _rangeMeter;
    [SerializeField]
    private Slider _speedMeter;
    [SerializeField]
    private Slider _damageMeter;
    [SerializeField]
    private Text _cost;
    [SerializeField]
    private GameObject _shop;

    private int _multiplier = 1;

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
        /* Minimum damage / range / speed = 0;. Maximum damage / range / speed = 10; */
        Weapon weaponComp = listWeapons[_curWeapon].GetComponent<Weapon>();

        _weaponName.text = weaponComp.name;
        _showingWeapon.GetComponent<Image>().sprite = listWeapons[_curWeapon].GetComponent<SpriteRenderer>().sprite;
        _cost.text = weaponComp.cost.ToString();

        _damageMeter.value = weaponComp.damage;
        _speedMeter.value = weaponComp.attackSpeed;
        _rangeMeter.value = weaponComp.range;
    }

    public void ChangeWeapon(int amount)
    {
        _curWeapon = _curWeapon + amount;

        if (_curWeapon > (listWeapons.Length - 1 ))
        {
            _curWeapon = 0;
        }
        else if (_curWeapon < 0)
        {
            _curWeapon = (listWeapons.Length -1);
        }

        SetWeapon();
    }

    public void BuyWeapon()
    {

    }
}
