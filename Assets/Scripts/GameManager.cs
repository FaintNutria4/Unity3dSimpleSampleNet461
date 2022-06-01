using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager gameManager;

    public enum GameState { MainMenu, Playing };

    private GameState gameState;

    private void Awake()
    {
        gameManager = this;
        gameState = GameState.MainMenu;
    }

    public void SetWallet(string publicKey, string privateKey)
    {

    }

    public void LoadItem()
    {

    }



}
