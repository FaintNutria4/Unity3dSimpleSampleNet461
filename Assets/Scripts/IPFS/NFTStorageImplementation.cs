using NFTStorage.JSONSerialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFTStorageImplementation : MonoBehaviour
{

    public NFTStorage.NFTStorageClient client;
    private bool a = true;

    // Start is called before the first frame update
    void Start()
    {
        Upload();
    }

    private void Update()
    {
        if(a){
            a = false;
            Download(); ;
        }
    }


    private async void Upload()
    {
        NFTStorageUploadResponse x;
        x = await client.UploadDataFromString("Test Text Hola Hola");
       if(x != null)
        {

            NFTStorageNFTObject nft = x.value;
            Debug.Log("The Cid is: " + nft.cid);
        }
        
    }

    private async void Download()
    {
        string cid = "bafkreig6kxesjo3pgmnrinavumqwyfcbonha7gpuo3ii7rvofiz2spkbc4";
        string x = await client.GetFileData(cid);

        Debug.Log(x);


    }

    
}
