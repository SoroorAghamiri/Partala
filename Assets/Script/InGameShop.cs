using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BazaarInAppBilling;
using UPersian.Components;
using GameAnalyticsSDK;

public class InGameShop : MonoBehaviour
{
    private int productIndex = 0;
    private int amountOfFeathersTobeAdded;

    [SerializeField] GameObject messageBox;
    [SerializeField] RtlText textMessage;


    [SerializeField] float coolDownForAd;
    float timer;
    [SerializeField] RtlText timerText;
    [SerializeField] Button featherWithAdButton;
    public void BuyProduct(int index)
    {
        productIndex = index;
        StoreHandler.instance.Purchase(productIndex, OnPurchaseFailed, OnPurchasedSuccessfully);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (AdManager.Instance.RequestRewardAd() == 5)
        {
            ShowMessage("دستگاه شما به اینترنت وصل نیست، برای تبلیغ دوباره تلاش کنید!");
        }

        timer = coolDownForAd;
        timer -= TimeMaster.Instance.CheckDate();
        if (timer > 0)
        {
            timerText.gameObject.SetActive(true);
            featherWithAdButton.interactable = false;
        }
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
                    amountOfFeathersTobeAdded = 5;
                    break;
                case 2:
                    amountOfFeathersTobeAdded = 9;
                    break;
                case 3:
                    amountOfFeathersTobeAdded = 15;
                    break;
                case 4:
                    amountOfFeathersTobeAdded = 20;
                    break;
            }
            GameAnalytics.NewBusinessEvent("rial", int.Parse(StoreHandler.instance.products[productIndex].price), "feathers", StoreHandler.instance.products[productIndex].productId, "InGameShop");
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "feather", amountOfFeathersTobeAdded, "purchase", "feather");
            DataManager.Instance.SetFeather(DataManager.Instance.GetFeather() + amountOfFeathersTobeAdded);
            ShowMessage(amountOfFeathersTobeAdded.ToString() + " پر به اکانت شما اضافه شد.");
        }
        else //A Non-Consumable Currency Was Bought
        {
            
            GameAnalytics.NewBusinessEvent("rial", int.Parse(StoreHandler.instance.products[productIndex].price), "NoAd", StoreHandler.instance.products[productIndex].productId, "InGameShop");
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
    private void ShowMessage(string message)
    {
        messageBox.SetActive(true);
        textMessage.text = message;
    }
    public void BuyFeatherWithAD()
    {
        StartCoroutine(CallingAdAndShowing());
    }
    IEnumerator CallingAdAndShowing()
    {
        if (AdManager.Instance.ShowRewardAd() == true)
        {
            ShowMessage("درحال بارگذاری تبلیغ...");
            yield return new WaitForSeconds(1f);
            if (AdManager.Instance.GetResultOfAd() == true)
            {
                ShowMessage("یک پر به حساب شما اضافه گردید.");
                DataManager.Instance.SetFeather(DataManager.Instance.GetFeather() + 1);
                GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "feather", 1, "adreward", "feather");
                ResetClock();
                timerText.gameObject.SetActive(true);
                featherWithAdButton.interactable = false;
            }
            else
            {
                ShowMessage("شما تبلیغ را کامل مشاهده نکردید یا خطایی رخ داده است. دوباره امتحان کنید.");
            }
        }
        else
        {
            ShowMessage("جادوگر باعث شده تبلیغی در دسترس نباشد، بعدا تلاش کنید!");
        }
    }
    private void Update()
    {
        ShowTimerForAdButton();
    }

    private void ShowTimerForAdButton()
    {
        timer -= Time.deltaTime;
        int tmp;
        if (timer > 3600f)
        {
            tmp = (int)timer / 3600;
            timerText.text = tmp.ToString() + " ساعت";
        }
        else if (timer > 60f)
        {
            tmp = (int)timer / 60;
            timerText.text = tmp.ToString() + " دقیقه";
        }
        else
        {
            tmp = (int)timer;
            timerText.text = tmp.ToString() + " ثانیه";
        }
        if (timer <= 0)
        {
            timerText.gameObject.SetActive(false);
            featherWithAdButton.interactable = true;
        }
    }

    void ResetClock()
    {
        TimeMaster.Instance.SaveDate();
        timer = coolDownForAd;
        timer -= TimeMaster.Instance.CheckDate();
    }
}
