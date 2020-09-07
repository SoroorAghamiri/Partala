using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BazaarPlugin;
public class ShopManager : MonoBehaviour
{
    public string RSAKey;
    public Products[] products;
    private int productIndex = 0;
    private void Awake()
    {
        BazaarIAB.init(RSAKey);
    }
    public void Purchase(int index)
    {
        productIndex = index;
        BazaarIAB.purchaseProduct(products[productIndex].Id);
    }
    
    private void OnEnable()
    {
        IABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
        IABEventManager.purchaseFailedEvent += purchaseFailedEvent;
    }
    private void OnDisable()
    {
        IABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
        IABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
    }
    //will be called when the purchase is successful
    void purchaseSucceededEvent(BazaarPurchase purchase)
    {
        DataManager.Instance.SetFeather(DataManager.Instance.GetFeather() + products[productIndex].amount);
    }
    //will be called when the purchase failed
    void purchaseFailedEvent(string error)
    {

    }
}



[System.Serializable]
public class Products
{
    public string Id;
    public int amount;
}
