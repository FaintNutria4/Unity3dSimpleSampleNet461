using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nethereum.Hex;
using Nethereum.Web3;
using Nethereum.JsonRpc.UnityClient;
using ItemStorage.ContractDefinition;
using System.Numerics;
using System;

public class EthereumAPI : MonoBehaviour
{
    [Header("Net")]
    //Ganache TestNet url
    public string netUrl = "http://localhost:7545";

    [Header("Contract Address")]
    //Contract ABI and address
    public string itemStorageAddress = "0x7853b6F17E730d46EfD914878779715cd70f24eD";

    [Header("Player Wallet")]
    //Test Account Keys
    public string publicKey = "0x99A556A0E54255e7A4FDE8046BE54394fB7dA17f";
    public string privateKey = "0xe9c30eb71f4c1d096149c8e964fa0818f00ad49ac80a7e6b1c51f5f32b614c2c";

    public IEnumerator GetBalances(Action<List<BigInteger>> callback)
    {
        //Query request using our acccount and the contracts address (no parameters needed and default values)
        var queryRequest = new QueryUnityRequest<GetBalancesFunction, GetBalancesOutputDTO>(netUrl, publicKey);        
        yield return queryRequest.Query(new GetBalancesFunction() { }, itemStorageAddress);

        //Getting the dto response already decoded
        var dtoResult = queryRequest.Result;
        List<BigInteger> balances = dtoResult.ReturnValue1;

        callback(balances);


    }

    public IEnumerator TransferItem(String newOwner, int idType, int amount, int gold)
    {
        Debug.Log("Transfer Item");
        Debug.Log("New Owner: "+ newOwner);
        Debug.Log("IdType: "+ idType);
        Debug.Log("amount "+ amount);
        Debug.Log("gold "+ gold);

        var transactionTransferRequest = new TransactionSignedUnityRequest(netUrl, privateKey);
        transactionTransferRequest.UseLegacyAsDefault = true;

        var info = new TransferItemToAddressFunction() { NewOwner = newOwner, IdType = idType, Amount = amount, Gold = gold};
        
        
        yield return transactionTransferRequest.SignAndSendTransaction(info, itemStorageAddress);

        var transactionTransferHash = transactionTransferRequest.Result;
        

    }

    public IEnumerator GetItemStats(BigInteger ItemId, Action<ItemStorage.ContractDefinition.Item> callback)
    {
        var queryRequest = new QueryUnityRequest<GetItemStatsFunction, GetItemStatsOutputDTO>(netUrl, publicKey);
        yield return queryRequest.Query(new GetItemStatsFunction() { IdType = ItemId }, itemStorageAddress);
        
        var result = queryRequest.Result;
        ItemStorage.ContractDefinition.Item item = result.ReturnValue1;

        callback(item);

    }
}
