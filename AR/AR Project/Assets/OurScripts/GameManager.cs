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

    [HideInInspector] public SwitchMonster currentMonster;

    public GameObject[] panels;

    private GameObject currentPanel;
   
    

    void Start()
    {  
        currentScene = SceneManager.GetActiveScene();
        // Debug.Log("Current Scene Name ->"+ currentScene.name);

        if (currentScene.name == "SelectionScreen")
            MonsterSelection();

        if(currentScene.name == "MainScene")
        {
            currentPanel = panels[0];

            for (int i = 0; i < 3; i++)
            {
                if (selectMonsters[i].monsterId == 0)
                {
                    selectMonsters[i].monsterModel = Instantiate(selectMonsters[i].gameObject, selectMonsters[i].spawnPoint[0].position, selectMonsters[i].spawnPoint[0].rotation) as GameObject;

                    selectMonsters[i].CreateMonster();

                    currentMonster = selectMonsters[i];
                }
            }
        }
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

        if (currentScene.name == "MainScene")
        {
           // PanelManager();


            Debug.Log("health" + currentMonster.monsterStats.health);
            Debug.Log("attack" + currentMonster.monsterStats.attack);
            Debug.Log("speed" + currentMonster.monsterStats.speed);
            Debug.Log("energy" + currentMonster.monsterStats.energy);
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
            selectMonsters[i].monsterModel = Instantiate(selectMonsters[i].gameObject, selectMonsters[i].spawnPoint[i].position, selectMonsters[i].spawnPoint[i].rotation) as GameObject;
            selectMonsters[i].monsterId = i;

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
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

  public  void PanelManager (GameObject panel){

        currentPanel = panel;

        if (currentPanel == panels[0])
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
            panels[3].SetActive(false);
            panels[4].SetActive(false);
            panels[5].SetActive(false);
        }
        else if (currentPanel == panels[1])
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
            panels[2].SetActive(false);
            panels[3].SetActive(false);
            panels[4].SetActive(false);
            panels[5].SetActive(false);
        }
        else if (currentPanel == panels[2])
        {
            panels[0].SetActive(false);
            panels[1].SetActive(false);
            panels[2].SetActive(true);
            panels[3].SetActive(false);
            panels[4].SetActive(false);
            panels[5].SetActive(false);
        }
        else if (currentPanel == panels[3])
        {
            panels[0].SetActive(false);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
            panels[3].SetActive(true);
            panels[4].SetActive(false);
            panels[5].SetActive(false);
        }
        else if (currentPanel == panels[4])
        {
            panels[0].SetActive(false);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
            panels[3].SetActive(false);
            panels[4].SetActive(true);
            panels[5].SetActive(false);
        }
        else if (currentPanel == panels[5])
        {
            panels[0].SetActive(false);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
            panels[3].SetActive(false);
            panels[4].SetActive(false);
            panels[5].SetActive(true);
        }

  }

}
