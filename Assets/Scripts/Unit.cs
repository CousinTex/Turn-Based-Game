using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxhp;
    public int currentHp;
    public int heal;
    
    public bool TakeDamage(int dmg)
    {
        currentHp -= dmg;

        if (currentHp <= 0)
            return true;
        else
            return false;
    }
    public void Heal(int heal)
    {
        currentHp += heal;
        if (currentHp > maxhp)
            currentHp = maxhp;
    }

}
