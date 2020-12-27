using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleLoader : MonoBehaviour
{
    [SerializeField] AssetBundle assetBundle;
    // Start is called before the first frame update
    private void Awake()
    {
        var myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/episode2level16");
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
        }
        else
        {
            var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("Episode2.Level16");
            Instantiate(prefab);
        }
    }
}
