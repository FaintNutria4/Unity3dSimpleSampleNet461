using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferAnswerGuy : MonoBehaviour
{
    GameManager gameManager;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(getRefences());

    }

    IEnumerator getRefences()
    {
        do
        {
            gameManager = GameManager.gameManager;
            yield return new WaitForSeconds(1);
        }
        while (gameManager == null);

        do
        {
            player = gameManager.player;
            yield return new WaitForSeconds(1);
        }
        while (player == null);


    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            gameManager.setState(GameManager.GameState.OfferAnswerMenu);
        }
    }
}
