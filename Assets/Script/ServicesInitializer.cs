using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using BazaarInAppBilling;
using TapsellSDK;

public class ServicesInitializer : MonoBehaviour
{
    [SerializeField] string TAPSELL_KEY;
    private void Awake()
    {
        StoreHandler.instance.InitializeBillingService();
        Tapsell.Initialize(TAPSELL_KEY);
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
