using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

class LoadAssetBundle : MonoBehaviour
{
    public string BundleURL;
    public string AssetName;
    IEnumerator Start()
    {
        // Download the file from the URL. It will not be saved in the Cache
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(BundleURL))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
                throw new Exception("WWW download had an error:" + www.error);
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            if (bundle != null)
            {
                string rootAssetPath = bundle.GetAllAssetNames()[0];
                GameObject arObject = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject, this.transform);
                bundle.Unload(false);
                //callback(arObject);
            }
            else
                Debug.Log("Invalid AssetBundle");
            // Unload the AssetBundles compressed contents to conserve memory
            

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
