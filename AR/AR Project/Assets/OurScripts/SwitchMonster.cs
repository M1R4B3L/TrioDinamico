using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchMonster : MonoBehaviour
{
    public Transform[] spawnPoint;
    [HideInInspector] public int monsterId;
    [HideInInspector] public GameObject monsterModel;


    public struct Stats
    {
        public int health;
        public int attack;
        public int speed;
        public int energy;
        public Stats(int health_, int attack_, int speed_, int energy_)
        {
            health = health_;
            attack = attack_;
            speed = speed_;
            energy = energy_;
        }
    }

    public Stats monsterStats;

    public void CreateMonster()
    {
        monsterStats = new Stats(Random.Range(1, 30), Random.Range(1, 30), Random.Range(1, 30), Random.Range(1, 30));
    }
}
