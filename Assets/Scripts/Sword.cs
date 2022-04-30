using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword :Item
{
    public int dmg; 

    public Sword(int id, string name, string description, int dmg) : base(id, name, description)
    {
        this.dmg = dmg;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
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
