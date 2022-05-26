using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nethereum.Hex;
using Nethereum.Web3;
using Nethereum.JsonRpc.UnityClient;
using ItemStorage.ContractDefinition;
using System.Numerics;

public class GanacheTest : MonoBehaviour
{
    //Contract ABI and address
    string itemStorageAddress = "0x6Fd43Ba253cd4186f7795d0f9f67e2179d551a55";

    //Ganache TestNet url
    string netUrl = "http://localhost:7545";

    //Test Account Keys
    string publicKey = "0x5b68b0e1c7aD8f688a778E91959dB689F2b6061c";
    string privateKey = "8432f73c342088ccbb58eaa955b35609557e24eebfafd70f65dd6483b2f02a08";


    public GameObject sword;
    public Transform itemHolder;
    public User user;
    public PlayerController pc;

    Coroutine co;
    
    // Start is called before the first frame update
    void Start()
    {
        

        co = StartCoroutine(GetItemCoroutine());

        GetItem();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetItem()
    {
        StartCoroutine(GetItemToUser());
    }

    public IEnumerator GetItemToUser()
    {
        var queryRequest = new QueryUnityRequest<GetItemStatsFunction, GetItemStatsOutputDTO>(netUrl, publicKey);
        yield return queryRequest.Query(new GetItemStatsFunction() { }, itemStorageAddress);

        var result = queryRequest.Result;
        ItemStorage.ContractDefinition.Item item = result.ReturnValue1;

        //Check item id

        Instantiate(sword, itemHolder);
        Sword mySword = sword.GetComponent<Sword>();

        mySword.iname = item.Name;
        mySword.description = item.Description;
        mySword.damage = (int)item.Damage;
        mySword.id = (int)item.IdType;

        user.inventory[0] = mySword;
        pc.currentItem = mySword;
       
    }

    public IEnumerator GetContract()
    {
        //Query request using our acccount and the contracts address (no parameters needed and default values)
        var queryRequest = new QueryUnityRequest<GetBalancesFunction, GetBalancesOutputDTO>(netUrl, publicKey);
        yield return queryRequest.Query(new GetBalancesFunction() { }, itemStorageAddress);

        //Getting the dto response already decoded
        var dtoResult = queryRequest.Result;
        List<BigInteger> balances = dtoResult.ReturnValue1;

       
    }

    public IEnumerator GetItemCoroutine()
    {
        var queryRequest = new QueryUnityRequest<GetItemStatsFunction, GetItemStatsOutputDTO>(netUrl, publicKey);
        yield return queryRequest.Query(new GetItemStatsFunction() { }, itemStorageAddress);

        var result = queryRequest.Result;
        ItemStorage.ContractDefinition.Item item = result.ReturnValue1;

    }
}
