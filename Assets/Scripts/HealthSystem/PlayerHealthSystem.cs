using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, ICanTakeDmg
{
    private static float maxHp = 100f;
    private float hp = 100f;

    public float getCurrentHealth()
    {
        return hp;
    }

    public float getMaxHealth()
    {
        return maxHp;
    }

    public void TakeDmg(float dmg)
    {
        hp -= dmg; 
    }

}
