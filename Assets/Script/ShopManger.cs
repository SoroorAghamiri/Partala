using System;
using System.Collections;
using System.Collections.Generic;
using BazaarInAppBilling;
using UnityEngine;
using UnityEngine.UI;

public class ShopManger : MonoBehaviour
{
    public Button[] ShopButtons=new Button[6];
    public void Awake()
    {
        StoreHandler.instance.InitializeBillingService();
    }

    void Start()
    {
        for (int i = 0; i < ShopButtons.Length; i++)
        {
            ShopButtons[0].onClick.AddListener(delegate { OnShopButtonsClick(i); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnServiceInitializedSuccessfully()

    {



    }



    private void OnServiceInitializationFailed(int errorCode, string message)

    {

        Debug.Log(errorCode);
        Debug.LogError(message);

    }
    private void OnPurchasedSuccessfully(Purchase purchase, int productIndex)

    {

        switch (productIndex)
        {
            case 0: AddFeather(1);
              break;
            case 1: AddFeather(19);
                break;
            case 2: AddFeather(33);
                break;
            case 3: AddFeather(85);
                break;
            case 4: AddFeather(100);
                break;
            default:
            Debug.Log("index is not in product list");
                break;
        }
        //TODO:pop up for success full Purchase
        
    }



    private void OnPurchaseFailed(int errorCode, string message)

    {
        
        //TODO:pop up for un success full Purchase

    }    

    public void GoToSetting()
    {
     //TODO:GOTO GAME SETTING    
    }

    public void GotoTolevel()
    {
        //TODO:GOTO GAME LEVEL
    }

    public void OnShopButtonsClick(int index)
    {
     
        StoreHandler.instance.Purchase(index, OnPurchaseFailed, OnPurchasedSuccessfully);
    
    }

    public void AddFeather(int numberOfFeather)
    {
        GameSys.Instans.SetFeather(GameSys.Instans.GetFeather()+numberOfFeather);   
    }
    
}
