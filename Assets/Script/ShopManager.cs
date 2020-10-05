using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BazaarInAppBilling;
public class ShopManager : MonoBehaviour
{
    private LevelLoader mylevelLoader;
    private int productIndex = 0;
    private int amountOfFeathersTobeAdded;

    [SerializeField] GameObject messageBox;
    [SerializeField] Text textMessage;
    public void BuyProduct(int index)
    {
        productIndex = index;
        StoreHandler.instance.Purchase(productIndex, OnPurchaseFailed, OnPurchasedSuccessfully);
    }

    private void Start()
    {
        mylevelLoader = FindObjectOfType<LevelLoader>();
    }

    private void OnPurchasedSuccessfully(Purchase purchase, int productIndex)
    {
        Debug.Log("Purchased Successfully");
        if (StoreHandler.instance.products[productIndex].type == Product.ProductType.Consumable) //A type of Currency is Bought
        {
            switch (productIndex) //Hard coding amount of feathers to consume is Bad Thing ***Change Later***
            {
                case 0:
                    amountOfFeathersTobeAdded = 1;
                    break;
                case 1:
                    amountOfFeathersTobeAdded = 19;
                    break;
                case 2:
                    amountOfFeathersTobeAdded = 33;
                    break;
                case 3:
                    amountOfFeathersTobeAdded = 85;
                    break;
                case 4:
                    amountOfFeathersTobeAdded = 100;
                    break;
            }
            DataManager.Instance.SetFeather(DataManager.Instance.GetFeather() + amountOfFeathersTobeAdded);
            ShowMessage(amountOfFeathersTobeAdded.ToString() + " پر به اکانت شما اضافه شد.");
        }
        else //A Non-Consumable Currency Was Bought
        {
            //Activate No Ads
            DataManager.Instance.SetNoAdActive();
            //Show Message That No Ads Has Been Activated
            ShowMessage("نسخه کامل بازی برای شما فعال شد.");
        }

    }



    private void OnPurchaseFailed(int errorCode, string message)
    {
        Debug.Log("Purchase Failed");
        switch (errorCode)
        {
            case 2:
                //Show Message
                //The user doesn't have cafebazaar installed;
                ShowMessage("شما کافه بازار را روی گوشی خود نصب نکردید.");
                break;
            case 5:
                //Show Message
                //The User Cancelled The Purchase;
                ShowMessage("شما خرید خود را لغو کردید.");
                break;
            case 6:
                //Check Inventory and Consume Product
                break;
            case 7:
                //Show Message
                //User did not Enter his account in cafebazaar
                ShowMessage("شما وارد اکانت خود در کافه بازار نشدید.");
                break;
            case 8:
                //The Item Is not in the Inventory
                break;
            case 9:
                //Validating purchase stuff?????? Should I do it or not
                break;
            case 10:
                //This purchase was not validated and the price will be returned
                break;
            case 14:
                //Show Message
                //Redo The Call Whatever It was the service has been initialized Successfully and redo stuff
                ShowMessage("جادوگر ارتباط با کافه بازار را مختل کرده بود، مشکل حل شده است و دوباره امتحان کنید!");
                break;
        }



    }

    private void OnConsumptionFailed(int errorCode, string message)
    {
        Debug.Log("On Consumption fail");
        switch (errorCode)
        {
            case 2:
                //Show Message
                //The user doesn't have cafebazaar installed;
                ShowMessage("شما کافه بازار را روی گوشی خود نصب نکردید.");
                break;
            case 5:
                //Show Message
                //The User Cancelled The Purchase;
                ShowMessage("شما خرید خود را لغو کردید.");
                break;
            case 6:
                //Check Inventory and Consume Product
                break;
            case 7:
                //Show Message
                //User did not Enter his account in cafebazaar
                ShowMessage("شما وارد اکانت خود در کافه بازار نشدید.");
                break;
            case 8:
                //The Item Is not in the Inventory
                break;
            case 9:
                //Validating purchase stuff?????? Should I do it or not
                break;
            case 10:
                //This purchase was not validated and the price will be returned
                break;
            case 14:
                //Show Message
                //Redo The Call Whatever It was the service has been initialized Successfully and redo stuff
                ShowMessage("جادوگر ارتباط با کافه بازار را مختل کرده بود، مشکل حل شده است و دوباره امتحان کنید!");
                break;
        }
    }
    private void OnConsumption(Purchase purchase, int productIndex) 
    {
        Debug.Log("On Consumption");

    }
    private void OnInventoryHadProduct(Purchase purchase, int productIndex)

    {
        //StoreHandler.instance.ConsumePurchase(purchase, productIndex, OnConsumptionFailed, OnConsumption);


    }



    private void OnInventoryCheckFailed(int errorCode, string message)

    {



    }

    private void ShowMessage(string message)
    {
        messageBox.SetActive(true);
        textMessage.text = message;
    }
    public void OnBackClicked()
    {
        if(DataManager.Instance.lastSceneIndex==0)
        {
            mylevelLoader.LoadLevel(SceneNames.Start);
        }
        else
        {

            int e = DataManager.Instance.lastSceneIndex;
            DataManager.Instance.lastSceneIndex = 0;
            mylevelLoader.LoadLevel(e);
        }
        
    }

    public void BuyFeatherWithAD()
    {
        StartCoroutine(AdCaller());

    }
    IEnumerator AdCaller()
    {
        AdManager.Instance.RequestRewardAd();
        yield return new WaitForSeconds(2.0f);
        AdManager.Instance.ShowRewardAd();
        yield return new WaitForSeconds(1.0f);
        if (AdManager.Instance.GetResultOfAd() == true)
        {
            ShowMessage("یک پر به حساب شما اضافه گردید.");
            DataManager.Instance.SetFeather(DataManager.Instance.GetFeather() + 1);
        }
        else
        {
            ShowMessage("شما تبلیغ را کامل مشاهده نکردید یا خطایی رخ داده است. دوباره امتحان کنید.");
        }
    }
}
