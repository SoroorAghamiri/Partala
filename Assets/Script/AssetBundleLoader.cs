using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoader : MonoBehaviour
{

    [SerializeField] string nameOfPrefab;


    private GameManger myGameManger;
    private WinCheckerMoreThan2Objects winchecker;
    private TouchManager touchManager;


    private void Start()
    {
        myGameManger = FindObjectOfType<GameManger>();
        winchecker = FindObjectOfType<WinCheckerMoreThan2Objects>();
        touchManager = FindObjectOfType<TouchManager>();
        GetAssetBundleAndLoadLevel();
    }
    private void GetAssetBundleAndLoadLevel()
    {

        // Get downloaded asset bundle
        AssetBundle bundle = DataManager.Instance.GetAssetBundle();
        var prefab = bundle.LoadAsset<GameObject>(nameOfPrefab);

        Instantiate(prefab, transform.position, Quaternion.identity, transform);//Fix This Later

        CallScripts();
        //Implement Calling Other Scripts

    }

    private void CallScripts()
    {
        myGameManger.GetComponent<GameManger>().enabled = true;

        touchManager.GetComponent<TouchManager>().ABActive = true;
        touchManager.GetComponent<TouchManager>().enabled = true;

        winchecker.GetComponent<WinCheckerMoreThan2Objects>().ABActive = true;
        winchecker.GetComponent<WinCheckerMoreThan2Objects>().enabled = true;
    }

}
