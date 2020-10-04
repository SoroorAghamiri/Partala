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
          resultOfAdreturned = result.rewarded;
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

    public void AdShow()
    {
        if (DataManager.Instance.GetnoAdflag())
        {
            return;
        }
        betweenAdCounter++;
        Debug.Log("betweenAdCounter:" + betweenAdCounter);
        switch ( betweenLevelsAd - betweenAdCounter     )
        {
            case 1:
                RequestInterstialAd();
                break;
            case 0:
                TapsellShowOptions showOptions = new TapsellShowOptions();
                Tapsell.ShowAd(tapsellAdInterstial, showOptions);
                betweenAdCounter = 0;
                break;
        }
    }
    public bool GetResultOfAd()
    {
        return resultOfAdreturned;
    }
}
