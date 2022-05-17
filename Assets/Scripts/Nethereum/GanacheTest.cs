using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nethereum.Hex;
using Nethereum.Web3;

public class GanacheTest : MonoBehaviour
{
    //Contract ABI and address
    string abi = @"[{ ""inputs"": [{""internalType"": ""address"",""name"": ""a"",""type"": ""address"" } ], ""name"": ""callName"", ""outputs"": [ {""internalType"": ""string"",""name"": """",""type"": ""string""}], ""stateMutability"": ""view"",""type"": ""function"",""constant"": true}]";
    string address = "0xAeC0bc74eE8fe5488265dAB0133A70254e843c97";

    //Ganache TestNet url
    string netUrl = "http://localhost:7545";

    //Test Account Keys
    string publicKey = "0x6A7dda1Eb87811Ea9b30451f6BdeF8c7Cb82b61A";
    string privateKey = "0x1b696901dee9f47faf72d34b100da43d523ae9911d203d6d2b8b94fd3eb158a2";

       
    Web3 myWeb; 
    
    // Start is called before the first frame update
    void Start()
    {
        myWeb = new Web3(netUrl);


        GetContract();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    async void GetBanlance()
    {
        var balance = await myWeb.Eth.GetBalance.SendRequestAsync("0xd814Ca7a5020f457474273edf6ccEA9dEB3afADE");
        

        Debug.Log("Your balance is " + balance);
    }

    async void GetContract()
    {
        var contract = myWeb.Eth.GetContract(abi, address);
        var callNameFunction = contract.GetFunction("callName");

        var itemStorageAddress = "0x5d5BeA6d4756e62537737d3CD194F2F5B9460A73";

        var itemName = await callNameFunction.CallAsync<string>(itemStorageAddress);

        Debug.Log(itemName);
    }
}
