using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuHandler : MonoBehaviour
{
    public EthereumAPI shopEthereum;
    private GameManager gameManager;
    private bool Lock = false;


    public User user;
    public Text goldText;

    private int goldInput = 0;
    private string gold;

    

    public void Start()
    {
        gameManager = GameManager.gameManager;

    }

    public void BuySword()
    {
        if (!Lock)
            {
                string newOwner = user.ethereum.publicKey;
                StartCoroutine(BuySword(newOwner));
            }
        
        
    }

    public void ExitShop()
    {
        gameManager.setState(GameManager.GameState.Playing);
        user.LoadItems();
    }

    public void askForGold()
    {
        StartCoroutine(user.ethereum.GetBalances(UpdateGold));
    }

    public void UpdateGold(List<BigInteger> balances)
    {
        string phrase = balances[0] + " G";
        goldText.text = phrase;
    }

    public void setGoldInput( string  gold)
    {
        goldInput = int.Parse(gold);
    
    }

    public IEnumerator BuySword(string newOwner)
    {
        Lock = true;
        yield return StartCoroutine(shopEthereum.TransferItem(newOwner, 1, 1, goldInput));
        yield return StartCoroutine(user.ethereum.GetBalances(UpdateGold));

        Lock = false;

        user.LoadItems();
    }
}
