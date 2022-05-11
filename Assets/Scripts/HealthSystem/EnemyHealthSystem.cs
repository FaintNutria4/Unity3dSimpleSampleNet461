using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, ICanTakeDmg
{

    private static float maxHp = 100f;
    [SerializeField]
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
        Debug.Log("Current Health: " + hp);
    }
}
