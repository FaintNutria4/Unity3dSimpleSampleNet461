using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nethereum.Hex;
using Nethereum.Web3;

public class GanacheTest : MonoBehaviour
{
    Web3 myWeb;
    
    // Start is called before the first frame update
    void Start()
    {
        myWeb = new Web3("http://localhost:7545");


        GetBanlance();
        
    }

    // Update is called once per frame
    void Update()
    {
        GetBanlance();
    }


    async void GetBanlance()
    {
        var balance = await myWeb.Eth.GetBalance.SendRequestAsync("0xd814Ca7a5020f457474273edf6ccEA9dEB3afADE");
        

        Debug.Log("Your balance is " + balance);
    }
}
