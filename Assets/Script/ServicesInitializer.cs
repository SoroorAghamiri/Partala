using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using BazaarInAppBilling;
using TapsellSDK;

public class ServicesInitializer : MonoBehaviour
{
    [SerializeField] private string TAPSELL_KEY;
    private void Awake()
    {


    }
    // Start is called before the first frame update
    void Start()
    {
        StoreHandler.instance.InitializeBillingService(OnServiceInitializationFailed, OnServiceInitializedSuccessfully);
        Tapsell.Initialize(TAPSELL_KEY);
        GameAnalytics.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnServiceInitializedSuccessfully()

    {

        Debug.Log("cafe bazaar Initialized succesfully");

    }



    private void OnServiceInitializationFailed(int errorCode, string message)

    {
        Debug.Log("cafe bazaar Error code :" + errorCode.ToString()) ;
        Debug.Log(message);


    }
}
