using UnityEngine;

public class ShopBuyButton : MonoBehaviour
{
    #region Vars

    public NPCShop shop;

    #endregion

    #region Methods

    public void OnBuyButtonClick()
    {
        shop.BuyWeapon();
    }

    #endregion
}
