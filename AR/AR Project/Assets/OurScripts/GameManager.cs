using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Tap to play Screen

    public GameObject target;
    public Text[] Texts;
    public GameObject[] monsterObject;
    public SwitchMonster[] selectMonsters;

    private Touch touch_info;

    private Scene currentScene;

    private GameObject currentMonster;

    void Start()
    {  
        currentScene = SceneManager.GetActiveScene();
        // Debug.Log("Current Scene Name ->"+ currentScene.name);

        if (currentScene.name == "SelectionScreen")
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
        for(int i = 0; i < selectMonsters.Length; ++i)
        {
            selectMonsters[i].monsterModel = Instantiate(monsterObject[i], selectMonsters[i].spawnPoint[i].position, selectMonsters[i].spawnPoint[i].rotation) as GameObject;
            selectMonsters[i].monsterId = i;

            selectMonsters[i].CreateMonster();

            selectMonsters[i].monsterModel.transform.SetParent(target.transform);

            Debug.Log("Monster id:");
            Debug.Log(selectMonsters[i].monsterId);
        }
    }
    public void SwitchMonsterLeft()
    {
        for (int i = 0; i < 3; ++i)
        {
            selectMonsters[i].monsterId++;
            if (selectMonsters[i].monsterId == 3)
                selectMonsters[i].monsterId = 0;

            selectMonsters[i].monsterModel.transform.position = selectMonsters[i].spawnPoint[selectMonsters[i].monsterId].position;
        }
    }
    public void SwitchMonsterRight()
    {
        for (int i = 0; i < 3; ++i)
        {
            selectMonsters[i].monsterId--;
            if (selectMonsters[i].monsterId < 0)
                selectMonsters[i].monsterId = 3 - 1;
   
            selectMonsters[i].monsterModel.transform.position = selectMonsters[i].spawnPoint[selectMonsters[i].monsterId].position;
        }

    }
    public void SelectMonster()
    {
        currentMonster = selectMonsters[0].monsterModel;

        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
