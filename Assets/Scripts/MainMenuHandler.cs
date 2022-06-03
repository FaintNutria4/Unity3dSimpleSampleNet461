using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    private GameManager gameManager;
    public User user;

    private string publicKey;
    private string privateKey;

    public void Start()
    {
        gameManager = GameManager.gameManager;
        
    }
    public void Login()
    {
        Debug.Log("setWallet");
        user.SetWallet(publicKey, privateKey);
        Debug.Log("loadItems");

        user.LoadItems();
        Debug.Log("changeState");

        gameManager.setState(GameManager.GameState.Playing);
    }

    public void setPublicKey( string publicKey)
    {
        this.publicKey = publicKey;
    }

    public void setPrivateKey( string privateKey)
    {
        this.privateKey = privateKey;
    }
}
