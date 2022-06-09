using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    private GameManager gameManager;
    public User user;

    public UnityEngine.UI.InputField publicK;
    public UnityEngine.UI.InputField privateK;
    private string publicKey;
    private string privateKey;

    public void Start()
    {
        gameManager = GameManager.gameManager;

        setPrivateKey(privateK.text);
        setPublicKey(publicK.text);

        
    }
    public void Login()
    {
        StartCoroutine(user.ethereum.Login(publicKey, privateKey, TrueLogin));
      
    }

    public void setPublicKey( string publicKey)
    {
       
        this.publicKey = publicKey;
    }

    public void setPrivateKey( string privateKey)
    {
        this.privateKey = privateKey;
    }

    public void TrueLogin()
    {
        user.SetWallet(publicKey, privateKey);
        user.LoadItems();


        gameManager.setState(GameManager.GameState.Playing);
    }
}
