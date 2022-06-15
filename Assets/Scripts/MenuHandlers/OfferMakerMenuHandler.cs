using ItemStorage.ContractDefinition;
using Nethereum.JsonRpc.UnityClient;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class OfferMakerMenuHandler : MonoBehaviour
{
    private string seller;
    private int itemId;
    private int amount;
    private int gold;

    public Dropdown dropdown;
    public Text goldText;
    public User user;

    private int auxInt;

    private void OnEnable()
    {
        setDropDownItems();
        askForGold();
    }

    

    public IEnumerator setDropDownItems()
    {

        auxInt = 0;
        StartCoroutine(getItemNumber());
        yield return new WaitUntil(()=> auxInt != 0);


        dropdown.options.Clear();
        for (int i = 1; i < auxInt; i++)
        {
            StartCoroutine(user.ethereum.GetItemStats(i, setItem));
        }
        

    }

    private IEnumerator getItemNumber()
    {
        var queryRequest = new QueryUnityRequest<GetItemsNumberFunction, GetItemsNumberOutputDTO>(user.ethereum.netUrl, user.ethereum.publicKey);
        yield return queryRequest.Query(new GetItemsNumberFunction() { }, user.ethereum.itemStorageAddress);

        //Getting the dto response already decoded
        var dtoResult = queryRequest.Result;
        auxInt = (int)dtoResult.ReturnValue1;
        yield return null;
    }

   

    private void setItem(ItemStorage.ContractDefinition.Item itemId)
    {
        dropdown.options.Add(new Dropdown.OptionData(itemId.Name));
    }

    public void setSeller(string seller)
    {
        this.seller = seller;
    }

    public void setItemId(int itemId)
    {
        this.itemId = itemId;
    }

    public void setAmount(int amount)
    {
        this.amount = amount;
    }

    public void setGold(int gold)
    {
        this.gold = gold;
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
}
