using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class User : MonoBehaviour
{
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
       

       // StartCoroutine(ethereum.TransferItem("0xB8358aBD76c830611bF41ef88071dbC02e54B016", 0 , 1));
    }

    public void SetWallet(string publicKey, string privateKey)
    {
        ethereum.publicKey = publicKey;
        ethereum.privateKey = privateKey;
    }

    public void LoadItems()
    {
        StartCoroutine(DownLoadItems());
    }

     public IEnumerator DownLoadItems()
    {
        balances = null;
        StartCoroutine(ethereum.GetBalances(setBalances));
        yield return new WaitUntil(() => balances != null);
        if (balances.Count == 0 ) Debug.Log("User has no Items");
        int itemId = 0;
        int inventoryPosition = 0;

        Debug.Log("Downloading Items");
        foreach(BigInteger i in balances)
        {

            Debug.Log("Item Id: " + itemId + " We have " + i);

            if (itemId == 0 || i == 0)
            {
                itemId++; continue;
            }
            
            item = null;
            auxItem = null;
            auxGameObject = null;

            StartCoroutine(ethereum.GetItemStats(itemId, setAuxItem));
            yield return new WaitUntil(() => item != null);
            StartCoroutine(ipfs.LoadModel(item.Cid, ItemHolder, setAuxGameObjectAddItemScript));
            yield return new WaitUntil(() => auxGameObject != null);
            inventory[inventoryPosition] = auxItem;

            inventoryPosition++;
            itemId++;
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
