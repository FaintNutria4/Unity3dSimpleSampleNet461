using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanTakeDmg
{
    void TakeDmg(float dmg);
    float getCurrentHealth();
    float getMaxHealth();
}
