using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public int id;
    public string wallet;
    public Item[] inventory = new Item[10];

    public void UseItem()
    {
        inventory[0].Use();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
