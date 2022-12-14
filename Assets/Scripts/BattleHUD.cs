using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider sliderHP;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Level: " + unit.unitLevel;
        sliderHP.maxValue = unit.maxhp;
        sliderHP.value = unit.currentHp;
    }

    public void SetHP(int hp)
    {
        sliderHP.value = hp;
    }
    

    
}
