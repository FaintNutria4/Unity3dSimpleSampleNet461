using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public int id;
    public string name;
    public string description;
    public int dmg;

    // Start is called before the first frame update
    void Start(int id, string name, string description, int dmg)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.dmg = dmg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
