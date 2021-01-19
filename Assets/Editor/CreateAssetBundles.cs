using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetBundles
{

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        AssetBundleManifest assetMf = BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
        //BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
        Hash128 hash128 = assetMf.GetAssetBundleHash("AssetBundles");
        string data = hash128.ToString();
        string path = "Assets/Resources/AssetInfo/AssetBundleInfo.txt";
        using (FileStream fileStream = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(data);
            }
        }
        UnityEditor.AssetDatabase.Refresh();    
    }
}