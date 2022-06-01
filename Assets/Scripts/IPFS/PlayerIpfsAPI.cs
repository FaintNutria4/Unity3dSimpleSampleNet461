using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerIpfsAPI : MonoBehaviour
{

    public IEnumerator LoadModel(string cid, Transform position, Action<GameObject> callback)
    {
        string BundleURL = "https://" + cid + ".ipfs.dweb.link/";

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
                GameObject arObject = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject, position);
                arObject.SetActive(false);

                bundle.Unload(false);
                callback(arObject);
            }
            else
                Debug.Log("Invalid AssetBundle");
            // Unload the AssetBundles compressed contents to conserve memory
            

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
