using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Tap to play Screen

    [HideInInspector] public Animator animatorMonstrz;
    public GameObject target;
    //public Text[] Texts;
    public GameObject[] monsterObject;
    public SwitchMonster[] selectMonsters;

    private Touch touch_info;

    private Scene currentScene;

    [HideInInspector] public SwitchMonster currentMonster;

    public GameObject[] panels;

    private GameObject currentPanel;

    public GameObject levelObject;
    private TextMeshProUGUI levelText;

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

                    selectMonsters[i].monsterModel.transform.SetParent(target.transform);

                    currentMonster = selectMonsters[i];
 
                }
            }

           
        }
    }

    void Update()
    {
        DrawLevel();

        if (Input.touchCount > 0 )
        {
            touch_info = Input.GetTouch(0);

            if (currentScene.name == "MainScene" && panels[2].activeSelf==true)
            {
                if (touch_info.phase == TouchPhase.Began)
                {
                    Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(raycast, out raycastHit))
                    {
                        animatorMonstrz = currentMonster.monsterModel.GetComponent<Animator>();
                        animatorMonstrz.SetTrigger("PetGiven");
                        currentMonster.monsterModel.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
                        Debug.Log("Hit");
                    }

                }
            }

            if (touch_info.phase == TouchPhase.Ended)
            {
                if (currentScene.name == "InitialScene")
                {
                    GoToNextScene();
                }
            }
        }

        if (currentScene.name == "MainScene")
        {
           // PanelManager();
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

            
            if (selectMonsters[i].monsterId == 0)
            {
                animatorMonstrz = selectMonsters[i].monsterModel.GetComponent<Animator>();
                animatorMonstrz.SetTrigger("FoodGiven");
            }
            
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

            if (selectMonsters[i].monsterId == 0)
            {
                animatorMonstrz = selectMonsters[i].monsterModel.GetComponent<Animator>();
                animatorMonstrz.SetTrigger("FoodGiven");
            }
        }

    }
    public void SelectMonster()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public  void PanelManager (GameObject panel){

        currentPanel = panel;

        //Main Panel
        if (currentPanel == panels[0])
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
            panels[3].SetActive(false);
            panels[4].SetActive(false);
            panels[5].SetActive(false);
        }
        //Stats Panel
        else if (currentPanel == panels[1])
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
            panels[2].SetActive(false);
            panels[3].SetActive(false);
            panels[4].SetActive(false);
            panels[5].SetActive(false);
        }
        //Pet Panel
        else if (currentPanel == panels[2])
        {
            panels[0].SetActive(false);
            panels[1].SetActive(false);
            panels[2].SetActive(true);
            panels[3].SetActive(false);
            panels[4].SetActive(false);
            panels[5].SetActive(false);
        }
        //Train Panel
        else if (currentPanel == panels[3])
        {
            panels[0].SetActive(false);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
            panels[3].SetActive(true);
            panels[4].SetActive(false);
            panels[5].SetActive(false);
        }
        //Feed Panel
        else if (currentPanel == panels[4])
        {
            panels[0].SetActive(false);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
            panels[3].SetActive(false);
            panels[4].SetActive(true);
            panels[5].SetActive(false);

        }
        //Fight Panel
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
    private void DrawLevel()
    {
        levelText = levelObject.GetComponent<TextMeshProUGUI>();
        levelText.text = "Level " + currentMonster.monsterStats.level.ToString();
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void MonstrzFed(string foodType)
    {
        if (foodType == "Meat")
        {
            animatorMonstrz = currentMonster.monsterModel.GetComponent<Animator>();
            animatorMonstrz.SetTrigger("MeatGiven");
            currentMonster.monsterStats.health += 5;
        }
        else {
            animatorMonstrz = currentMonster.monsterModel.GetComponent<Animator>();
            animatorMonstrz.SetTrigger("PotatoGiven");
            currentMonster.monsterStats.energy += 5;
        }
    }

    public void MonstrzPet()
    {
        animatorMonstrz = currentMonster.monsterModel.GetComponent<Animator>();
        animatorMonstrz.SetTrigger("PetGiven");
        currentMonster.monsterStats.level++;
    }
}
