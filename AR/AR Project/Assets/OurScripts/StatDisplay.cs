using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatDisplay : MonoBehaviour
{

    public GameManager gameManager;

    

    public Slider[] sliders; // 0-> HP, 1->ATTACK, 2-> SPEED, 3-> ENERGY
    
    void Start()
    {
        
    }


    void Update()
    {
        IncreaseStats();
    }

    public void IncreaseStats()
    {
     
        sliders[0].value = gameManager.currentMonster.monsterStats.health;
        sliders[1].value = gameManager.currentMonster.monsterStats.attack;
        sliders[2].value = gameManager.currentMonster.monsterStats.speed;
        sliders[3].value = gameManager.currentMonster.monsterStats.energy;

    }

}
