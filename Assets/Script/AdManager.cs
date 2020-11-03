using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapsellSDK;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    [SerializeField] private string InterstialAdKey;
    [SerializeField] private string RewardAdKey;

    private TapsellAd tapsellAdInterstial;
    private TapsellAd tapsellAdReward;

    [SerializeField] private int betweenLevelsAd;
    private int betweenAdCounter;
    private bool interstialAdAvailable = false;
    private bool rewardAdAvailable = false;

    private bool resultOfAdreturned;
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
        Debug.Log("betweenAdCounter:" + betweenAdCounter);
        Tapsell.SetRewardListener(
      (TapsellAdFinishedResult result) =>
      {
          Debug.Log(
            "adId:" + result.adId + ", " +
            "zoneId:" + result.zoneId + ", " +
            "completed:" + result.completed + ", " +
            "rewarded:" + result.rewarded);
          if (result.rewarded && result.completed)
              resultOfAdreturned = true;
          else
              resultOfAdreturned = false;
      }

    );
    }
    public int RequestRewardAd()
    {
        int tmperror=-1;
        Tapsell.RequestAd(RewardAdKey, true,
          (TapsellAd result) =>
          {
              // onAdAvailable
              Debug.Log("on Ad Available");
              tapsellAdReward = result;
              rewardAdAvailable = true;
              tmperror = 0;
          },

          (string zoneId) =>
          {
              // onNoAdAvailable
              Debug.Log("no Ad Available");
              rewardAdAvailable = false;
              tmperror = 1;
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
              tmperror = 5;
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
        return tmperror;
    }
    public bool ShowRewardAd()
    {
        if(rewardAdAvailable==true)
        {
            Tapsell.ShowAd(tapsellAdReward, new TapsellShowOptions());
            rewardAdAvailable = false;
            return true;
        }
        rewardAdAvailable = false;
        return false;
        
    }

    public void RequestInterstialAd()
    {
        
        Tapsell.RequestAd(InterstialAdKey, true,
          (TapsellAd result) =>
          {
              // onAdAvailable
              Debug.Log("on Ad Available");
              tapsellAdInterstial = result;
              interstialAdAvailable = true;
              
          },

          (string zoneId) =>
          {
              // onNoAdAvailable
              Debug.Log("no Ad Available");
              interstialAdAvailable = false;
          },

          (TapsellError error) =>
          {
              // onError
              Debug.Log(error.message);
          },

          (string zoneId) =>
          {
              
              //Debug.Log("no Network");
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

    public void AdShow()
    {
        if (DataManager.Instance.GetnoAdflag())
        {
            return;
        }
        betweenAdCounter++;
        Debug.Log("betweenAdCounter:" + betweenAdCounter);
        switch (betweenLevelsAd - betweenAdCounter)
        {
            case 1:
                RequestInterstialAd();
                break;
            case 0:
                if (interstialAdAvailable == true)
                {
                    TapsellShowOptions showOptions = new TapsellShowOptions();
                    Tapsell.ShowAd(tapsellAdInterstial, showOptions);
                }
                betweenAdCounter = 0;
                interstialAdAvailable = false;
                break;
        }
    }
    public bool GetResultOfAd()
    {
        return resultOfAdreturned;
    }
}
