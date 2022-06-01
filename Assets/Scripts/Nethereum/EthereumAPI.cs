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
    //Contract ABI and address
    string itemStorageAddress = "0x6Fd43Ba253cd4186f7795d0f9f67e2179d551a55";

    //Ganache TestNet url
    string netUrl = "http://localhost:7545";

    //Test Account Keys
    string publicKey = "0x5b68b0e1c7aD8f688a778E91959dB689F2b6061c";
    string privateKey = "8432f73c342088ccbb58eaa955b35609557e24eebfafd70f65dd6483b2f02a08";

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

    public IEnumerator GetItemStats(BigInteger ItemId, Action<ItemStorage.ContractDefinition.Item> callback)
    {
        var queryRequest = new QueryUnityRequest<GetItemStatsFunction, GetItemStatsOutputDTO>(netUrl, publicKey);
        yield return queryRequest.Query(new GetItemStatsFunction() { IdType = ItemId }, itemStorageAddress);

        var result = queryRequest.Result;
        ItemStorage.ContractDefinition.Item item = result.ReturnValue1;

        callback(item);

    }
}
