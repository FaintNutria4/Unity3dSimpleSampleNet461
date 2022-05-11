using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword :Item
{
   

    public Sword(int id, string name, string description, int damage, int distance) : base(id, name, description)
    {
        this.damage = damage;
        this.distance = distance;
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
