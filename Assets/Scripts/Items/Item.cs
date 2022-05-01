using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{

    public int id;
    public string iname;
    public string description;
    

    
    public Item(int id, string name, string description)
    {
        this.id = id;
        this.iname = name;
        this.description = description;
    }

    public abstract void Use();
}
