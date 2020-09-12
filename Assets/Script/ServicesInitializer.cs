using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using BazaarInAppBilling;

public class ServicesInitializer : MonoBehaviour
{
    private void Awake()
    {
        StoreHandler.instance.InitializeBillingService();
        GameAnalytics.Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
