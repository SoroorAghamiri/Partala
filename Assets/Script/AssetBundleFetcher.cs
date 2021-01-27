using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleFetcher : MonoBehaviour
{
    [SerializeField] string BundleURL;
    [SerializeField] string AssetName;
    [SerializeField] string hashCode;
    [SerializeField] uint crc;
    // Start is called before the first frame update
    void Start()
    {
        //Call A MessageBox Saying We're Getting Ready To Download Levels
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
                //Implement A Message Saying There's a network Error And Try again Later, And Force Them to Leave The Level Selector
                Debug.Log(uwr.error);
            }
            else
            {
                //Allow The Player To Play As Normal
                // Get downloaded asset bundle
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                //Give This Bundle to Data Manager
                DataManager.Instance.SetAssetBundle(bundle);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
