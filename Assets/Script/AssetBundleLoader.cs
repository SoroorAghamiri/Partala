using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoader : MonoBehaviour
{
    public string BundleURL;
    public string AssetName;
    public string hashCode;
    public uint crc;

    private GameManger myGameManger;
    private WinCheckerMoreThan2Objects winchecker;
    private TouchManager touchManager;

    // Start is called before the first frame update
    //private void Awake()
    //{
    //    var myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/episode2level16");
    //    if (myLoadedAssetBundle == null)
    //    {
    //        Debug.Log("Failed to load AssetBundle!");
    //    }
    //    else
    //    {
    //        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("Episode2.Level16");
    //        Instantiate(prefab);
    //    }
    //}
    private void Start()
    {
        myGameManger = FindObjectOfType<GameManger>();
        winchecker = FindObjectOfType<WinCheckerMoreThan2Objects>();
        touchManager = FindObjectOfType<TouchManager>();
        StartCoroutine(DownloadAndCache());
    }
    IEnumerator DownloadAndCache()
    {
        CachedAssetBundle cachedAssetBundle = new CachedAssetBundle();
        Hash128 hash = Hash128.Parse(hashCode);

        cachedAssetBundle.hash = hash;
        cachedAssetBundle.name = AssetName;
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(BundleURL, cachedAssetBundle, crc))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                var prefab = bundle.LoadAsset<GameObject>("Episode2.Level16");

                Instantiate(prefab, transform.position, Quaternion.identity, transform);

                CallScripts();
                //Implement Calling Other Scripts
            }
        }
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
