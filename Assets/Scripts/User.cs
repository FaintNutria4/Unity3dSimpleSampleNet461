using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class User : MonoBehaviour
{
    [Header("User Info")]
    
    public string publicKey;
    public string privateKey;

    [Header("IPFS API")]

    public PlayerIpfsAPI ipfs;

    [Header("Ethrereum API")]

    public EthereumAPI ethereum;

    [Header("Items")]

    public Item currentItem;    
    public Transform ItemHolder;
    public Item[] inventory = new Item[10];

   

    List<BigInteger> balances;
    ItemStorage.ContractDefinition.Item item;
    Item auxItem;
    GameObject auxGameObject;

    public void Start()
    {
        StartCoroutine(DownLoadItems());
    }

     IEnumerator DownLoadItems()
    {
        balances = null;
        StartCoroutine(ethereum.GetBalances(setBalances));
        yield return new WaitUntil(() => balances != null);
        if (balances.Count == 0 ) Debug.Log("User has no Items");
        int o = 0;
        foreach(BigInteger i in balances)
        {
            if (i == 0) continue;
            item = null;
            auxItem = null;
            auxGameObject = null;

            StartCoroutine(ethereum.GetItemStats(o, setAuxItem));
            yield return new WaitUntil(() => item != null);
            StartCoroutine(ipfs.LoadModel(item.Cid, ItemHolder, setAuxGameObjectAddItemScript));
            yield return new WaitUntil(() => auxGameObject != null);
            inventory[o] = auxItem;
            o++;
        }

        if (inventory[0] != null)
        {
            inventory[0].gameObject.SetActive(true);
            currentItem = inventory[0];
        }
        

    }

   
    //Add Item's Script
    private void setAuxGameObjectAddItemScript(GameObject gameObject)
    {
        int itemId = (int)item.IdType;
        
        switch (itemId)
        {
            default:
                Sword sword = gameObject.AddComponent<Sword>();
                sword.iname = item.Name;
                sword.description = item.Description;
                sword.damage = (int)item.Damage;
                sword.id = (int)item.IdType;
                sword.distance = 3;

                auxItem = sword;
                break;

        }
        
        
        auxGameObject = gameObject;
    }

    private void setAuxItem(ItemStorage.ContractDefinition.Item item)
    {
        this.item = item;
    }

    private void setBalances(List<BigInteger> newBalances)
    {
        balances = newBalances;
    }



}
