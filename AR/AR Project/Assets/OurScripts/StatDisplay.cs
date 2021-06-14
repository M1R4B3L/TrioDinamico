using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StatDisplay : MonoBehaviour
{

    public GameManager gameManager;

    private Scene currentScene;

    public Slider[] sliders; // 0-> HP, 1->ATTACK, 2-> SPEED, 3-> ENERGY
    
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }


    void Update()
    {
        IncreaseStats();
    }

    public void IncreaseStats()
    {
        if (currentScene.name == "CombatScene")
        {
            sliders[0].value = gameManager.currentMonster.monsterStats.health;
            sliders[1].value = gameManager.enemyMonster.monsterStats.health;
        }
        else
        {
            sliders[0].value = gameManager.currentMonster.monsterStats.health;
            sliders[1].value = gameManager.currentMonster.monsterStats.attack;
            sliders[2].value = gameManager.currentMonster.monsterStats.speed;
            sliders[3].value = gameManager.currentMonster.monsterStats.energy;
        }

    }

}
