using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BazaarInAppBilling;
public class ShopManager : MonoBehaviour
{
    private int productIndex = 0;
    [SerializeField] Text[] debugTxts;
    int txtIndex = 0;
    public void BuyProduct(int index)
    {
        productIndex = index;
        StoreHandler.instance.Purchase(productIndex, OnPurchaseFailed, OnPurchasedSuccessfully);
        PrintOutDebug("Purchase Was Called");
    }

    private void PrintOutDebug(string message)
    {
        debugTxts[txtIndex].text = message;
        txtIndex++;
        if (txtIndex == 4)
            txtIndex = 0;
    }

    private void OnPurchasedSuccessfully(Purchase purchase, int productIndex)
    {
        PrintOutDebug("Purchase Was Succesful");
        StoreHandler.instance.ConsumePurchase(purchase, productIndex, OnConsumptionFailed, OnConsumedSuccesfully);

    }



    private void OnPurchaseFailed(int errorCode, string message)

    {
        PrintOutDebug("Purchase Failed  erorrcode " + errorCode.ToString());
        PrintOutDebug(message);


    }

    private void OnConsumptionFailed(int errorCode, string message)
    {
        PrintOutDebug("Failed Consume erorrcode " + errorCode.ToString());
        PrintOutDebug(message);


    }
    private void OnConsumedSuccesfully(Purchase purchase, int productindex)
    {
        PrintOutDebug("Successful Consume");



    }
}
