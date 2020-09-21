using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapsellSDK;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    [SerializeField] private string InterstialAdKey;
    [SerializeField] private string RewardAdKey;

    private TapsellAd tapsellAdInterstial;
    private TapsellAd tapsellAdReward;

    [SerializeField] private int betweenLevelsAd;
    private int betweenAdCounter;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        betweenAdCounter = 0;
        Tapsell.SetRewardListener(
      (TapsellAdFinishedResult result) =>
      {
          Debug.Log(
            "adId:" + result.adId + ", " +
            "zoneId:" + result.zoneId + ", " +
            "completed:" + result.completed + ", " +
            "rewarded:" + result.rewarded);
      }
    );
    }
    public void RequestRewardAd()
    {

        Tapsell.RequestAd(RewardAdKey, true,
          (TapsellAd result) => {
          // onAdAvailable
          Debug.Log("on Ad Available");
              tapsellAdReward = result;
          },

          (string zoneId) => {
          // onNoAdAvailable
          Debug.Log("no Ad Available");
          },

          (TapsellError error) => {
          // onError
          Debug.Log(error.message);
          },

          (string zoneId) => {
          // onNoNetwork
          Debug.Log("no Network");
          },

          (TapsellAd result) => {
          // onExpiring
          Debug.Log("expiring");
          },

          (TapsellAd result) => {
          // onOpen
          Debug.Log("open");
          },

          (TapsellAd result) => {
          // onClose
          Debug.Log("close");
          }

        );
    }
    public void ShowRewardAd()
    {
        Tapsell.ShowAd(tapsellAdReward, new TapsellShowOptions());
    }

    public void RequestInterstialAd()
    {

        Tapsell.RequestAd(InterstialAdKey, true,
          (TapsellAd result) =>
          {
              // onAdAvailable
              Debug.Log("on Ad Available");
              tapsellAdInterstial = result;
          },

          (string zoneId) =>
          {
              // onNoAdAvailable
              Debug.Log("no Ad Available");
          },

          (TapsellError error) =>
          {
              // onError
              Debug.Log(error.message);
          },

          (string zoneId) =>
          {
              // onNoNetwork
              Debug.Log("no Network");
          },

          (TapsellAd result) =>
          {
              // onExpiring
              Debug.Log("expiring");
          },

          (TapsellAd result) =>
          {
              // onOpen
              Debug.Log("open");
          },

          (TapsellAd result) =>
          {
              // onClose
              Debug.Log("close");
          }

        );
    }

    //private void RequestAdFromTapsell()
    //{
    //    Debug.Log("Request ad from tapsell is calling");
    //    Tapsell.RequestAd(rewardAdKey, false, onAdAvailableAction, onNoAdAvailableAction, onErrorAction, onNoNetworkAction, onExpiringAction);
    //}

    //private void onExpiringAction(TapsellAd tapsellAd)
    //{
    //    Debug.Log("Expired Tapsell Ad");
    //}

    //private void onAdAvailableAction(TapsellAd tapsellAd)
    //{
    //    Debug.Log("Ad is available");
    //    TapsellShowOptions showOptions = new TapsellShowOptions();
    //    showOptions.backDisabled = true;
    //    showOptions.immersiveMode = false;
    //    showOptions.rotationMode = TapsellShowOptions.ROTATION_UNLOCKED;
    //    showOptions.showDialog = true;


    //    Tapsell.ShowAd(tapsellAd, showOptions);
    //}
    //private void onNoAdAvailableAction(string message)
    //{
    //    Debug.Log("No Ad Available");
    //}
    //private void onErrorAction(TapsellError tapsellError)
    //{
    //    Debug.Log("Error");
    //}
    //private void onNoNetworkAction(string message)
    //{
    //    Debug.Log("onNoNetwork");
    //}


    public void AdShow()
    {
        betweenAdCounter++;
        switch (betweenAdCounter)
        {
            case 1:
                RequestInterstialAd();
                break;
            case 2:
                TapsellShowOptions showOptions = new TapsellShowOptions();
                //showOptions.backDisabled = true;
                Tapsell.ShowAd(tapsellAdInterstial, showOptions);
                betweenAdCounter = 0;
                break;
        }
    }
}
