using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager gameManager;

    public GameObject mainMenu;
    public GameObject shopMenu;
    public GameObject OfferMakerMenu;
    public GameObject OfferAnswerMenu;


    public GameObject player;

    

    public enum GameState { MainMenu, Playing, ShopMenu, OfferMakerMenu, OfferAnswerMenu };

    public GameState gameState;

    private void Awake()
    {
        gameManager = this;
        gameState = GameState.MainMenu;
        Time.timeScale = 0;
    }

    public void setState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                Time.timeScale = 0;
                mainMenu.SetActive(true);
                mainMenu.transform.parent.gameObject.SetActive(true);

                break;
            case GameState.Playing:
                Time.timeScale = 1;
                mainMenu.SetActive(false);
                shopMenu.SetActive(false);
                shopMenu.transform.parent.gameObject.SetActive(false); //Canvas
                
                break;
            case GameState.ShopMenu:
                Time.timeScale = 0;
                shopMenu.SetActive(true);
                shopMenu.transform.parent.gameObject.SetActive(true);
                shopMenu.GetComponent<ShopMenuHandler>().askForGold();
                break;
            case GameState.OfferMakerMenu:
                Time.timeScale = 0;
                OfferMakerMenu.SetActive(true);
                OfferMakerMenu.transform.parent.gameObject.SetActive(true);
              //  OfferMakerMenu.GetComponent<OfferMakerMenuHandler>().askForGold();
                break;
            case GameState.OfferAnswerMenu:
                Time.timeScale = 0;
                OfferAnswerMenu.SetActive(true);
                OfferAnswerMenu.transform.parent.gameObject.SetActive(true);
                OfferAnswerMenu.GetComponent<ShopMenuHandler>().askForGold();
                break;
        }

        gameState = state;
    }

    

    



}
