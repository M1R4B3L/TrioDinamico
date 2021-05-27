using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchMonster : MonoBehaviour
{
    public Transform[] spawnPoint;
    [HideInInspector] public int monsterId;
    [HideInInspector] public GameObject monsterModel;


    struct Stats
    {
        int health;
        int armor;
        int speed;
        int energy;
        public Stats(int health_, int armor_, int speed_, int energy_)
        {
            health = health_;
            armor = armor_;
            speed = speed_;
            energy = energy_;
        }
    }

    private Stats monsterStats;

    public void CreateMonster()
    {
        monsterStats = new Stats(Random.Range(1, 30), Random.Range(1, 30), Random.Range(1, 30), Random.Range(1, 30));
    }
}
