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

    private int _multiplier = 1;

    private Vector2 _rangeStartLoc;
    private Vector2 _speedStartLoc;
    private Vector2 _damageStartLoc;

    void Start()
    {
        _damageStartLoc = _damageMeter.transform.position;
        _rangeStartLoc = _rangeMeter.transform.position;
        _speedStartLoc = _speedMeter.transform.position;
    }

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
        Reset();

        Weapon weaponComp = listWeapons[_curWeapon].GetComponent<Weapon>();

        _weaponName.text = weaponComp.name;
        _showingWeapon.GetComponent<Image>().sprite = listWeapons[_curWeapon].GetComponent<SpriteRenderer>().sprite;
        _cost.text = weaponComp.cost.ToString();

        _rangeMeter.transform.localScale = new Vector2(weaponComp.range / _multiplier, _rangeMeter.transform.lossyScale.y);
        _speedMeter.transform.localScale = new Vector2(weaponComp.attackSpeed / _multiplier, _speedMeter.transform.lossyScale.y);
        _damageMeter.transform.localScale = new Vector2(weaponComp.damage / _multiplier, _damageMeter.transform.lossyScale.y);

        _rangeMeter.transform.position = new Vector2(_rangeMeter.transform.position.x + 14 * _rangeMeter.transform.localScale.x , _rangeMeter.transform.position.y);
        _speedMeter.transform.position = new Vector2(_speedMeter.transform.position.x + 14 * _speedMeter.transform.localScale.x , _speedMeter.transform.position.y);
        _damageMeter.transform.position = new Vector2(_damageMeter.transform.position.x + 14 * _damageMeter.transform.localScale.x, _damageMeter.transform.position.y);
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

    void Reset()
    {
        _rangeMeter.transform.position = _rangeStartLoc;
        _speedMeter.transform.position = _speedStartLoc;
        _damageMeter.transform.position = _damageStartLoc;
    }
}
