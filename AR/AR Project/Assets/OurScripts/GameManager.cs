using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Tap to play Screen

    public Text[] Texts;
    public GameObject[] monsterObject;
    public SwitchMonster[] selectMonsters;

    private Touch touch_info;

    private Scene currentScene;



    void Start()
    {  
        currentScene = SceneManager.GetActiveScene();
        // Debug.Log("Current Scene Name ->"+ currentScene.name);

        MonsterSelection();
    }

    void Update()
    {

        if (Input.touchCount > 0)
        {
            touch_info = Input.GetTouch(0);
            
            for(int text_num=0; text_num< Texts.Length; ++text_num)
            {

                Texts[text_num].color = Color.red;
                
            }

            if (touch_info.phase == TouchPhase.Ended)
            {
                
                GoToNextScene();

            }

        }
        else
        {
            for (int text_num = 0; text_num < Texts.Length; ++text_num)
            {

                Texts[text_num].color = Color.white;

            }
        }
      
    }

    void GoToNextScene()
    {

        if (currentScene.name == "InitialScene")
        {
            SceneManager.LoadScene("SelectionScreen", LoadSceneMode.Single);
        }
        else if (currentScene.name == "SelectionScreen")
        {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }

    }

    private void MonsterSelection()
    {
        for(int i = 0; i < selectMonsters.Length; i++)
        {
            selectMonsters[i].monsterModel = Instantiate(monsterObject[i], selectMonsters[i].spawnPoint.position, selectMonsters[i].spawnPoint.rotation) as GameObject;
            selectMonsters[i].monsterId = i++;

            selectMonsters[i].CreateMonster();

            Debug.Log(selectMonsters[i].monsterId);
        }
    }
}
