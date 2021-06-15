using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Vuforia;

public class GameManager : MonoBehaviour
{
    //Tap to play Screen

    [HideInInspector] public Animator animatorMonstrz;
    [HideInInspector] public Animator animatorEnemyMonstrz;
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
    public GameObject selectButton;


    //COMBAT SCENE

    public GameObject[] fightPositions;
    [HideInInspector] public SwitchMonster enemyMonster;
    private bool playerWin;
    private bool fightComplete=false;
    private int originalHP;
    public Slider playerSlider;
    public Slider enemySlider;

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
                if (selectMonsters[i].monsterId == 0) //PLAYER MONSTER SELECTION
                {
                    selectMonsters[i].monsterModel = Instantiate(selectMonsters[i].gameObject, selectMonsters[i].spawnPoint[0].position, selectMonsters[i].spawnPoint[0].rotation) as GameObject;

                    selectMonsters[i].CreateMonster();

                    selectMonsters[i].monsterModel.transform.SetParent(target.transform);

                    currentMonster = selectMonsters[i];
 
                }
            }
        }

        if (currentScene.name == "CombatScene") 
        {
            for (int i = 0; i < 3; i++) //PLAYER MONSTER SELECTION
            {
                if (selectMonsters[i].monsterId == 0)
                {
                    



                    selectMonsters[i].monsterModel = Instantiate(selectMonsters[i].gameObject, fightPositions[0].transform.position, selectMonsters[i].spawnPoint[0].rotation) as GameObject;

                    selectMonsters[i].CreateMonster();

                    selectMonsters[i].monsterModel.transform.SetParent(target.transform);

                    selectMonsters[i].monsterModel.transform.Rotate(0, -90, 0);
                    currentMonster = selectMonsters[i];

                    if (currentMonster.monsterStats.health < 30)
                    {
                        currentMonster.monsterStats.health = 30;
                    }

                    originalHP = currentMonster.monsterStats.health;

                    playerSlider.maxValue=playerSlider.value= currentMonster.monsterStats.health;


                    if (i == 0) { //ENEMY MONSTER SELECTION

                        SelectEnemyType(1,2,0);

                    }
                    else if (i == 1)
                    {

                        SelectEnemyType(0, 2,1);

                    }
                    else
                    {
                        SelectEnemyType(0, 1,2);
                    }

                }

               
            }
            
           
        }
    }

    void Update()
    {
     
        if (currentScene.name == "MainScene")
        {
            DrawLevel();
        }

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


        if (currentScene.name == "CombatScene")
        {
            if (fightComplete == false)
            {

                animatorMonstrz = currentMonster.monsterModel.GetComponent<Animator>();
                if (animatorMonstrz.GetCurrentAnimatorStateInfo(0).IsName("Idle03"))
                {
                    if (fightComplete == false)
                    {
                        panels[1].SetActive(true);
                    }
                }
                else
                {

                    panels[1].SetActive(false);
                }
            }
            else
            {
                panels[4].SetActive(false);

                panels[0].SetActive(true);
                if (playerWin == true)
                {
                    panels[2].SetActive(true);
                }
                else
                {
                    panels[3].SetActive(true);
                }

            }
        }

    }

    
    public void GoToNextScene()
    {

        if (currentScene.name == "InitialScene")
        {
            SceneManager.LoadScene("SelectionScreen", LoadSceneMode.Single);
        }
        else if (currentScene.name == "SelectionScreen")
        {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
        else if (currentScene.name == "MainScene")
        {
            SceneManager.LoadScene("CombatScene", LoadSceneMode.Single);
        }
        else if (currentScene.name == "CombatScene")
        {
            currentMonster.monsterStats.health = originalHP;
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }

    }

    public void CombatManager(string moveType)
    {

        panels[1].SetActive(false);

        if(fightComplete == false) { 

            if (moveType == "attack")
            {
                animatorMonstrz = currentMonster.monsterModel.GetComponent<Animator>();
                animatorMonstrz.SetTrigger("AttackAction");
            }
            else if (moveType == "dodge")
            {
                animatorMonstrz = currentMonster.monsterModel.GetComponent<Animator>();
                animatorMonstrz.SetTrigger("DodgeAction");
            }
            else if (moveType == "block")
            {
                animatorMonstrz = currentMonster.monsterModel.GetComponent<Animator>();
                animatorMonstrz.SetTrigger("BlockAction");
            }
            else
            {
                animatorMonstrz = currentMonster.monsterModel.GetComponent<Animator>();
                animatorMonstrz.SetTrigger("GrabAction");
            }
           
            int attackEnemyChosen;
            attackEnemyChosen = Random.Range(0, 4); //attack,dodge,block,grab
           
            if (attackEnemyChosen == 0)
            {
                animatorEnemyMonstrz = enemyMonster.monsterModel.GetComponent<Animator>();
                animatorEnemyMonstrz.SetTrigger("AttackAction");
            }
            else if (attackEnemyChosen == 1)
            {
                animatorEnemyMonstrz = enemyMonster.monsterModel.GetComponent<Animator>();
                animatorEnemyMonstrz.SetTrigger("DodgeAction");
            }
            else if (attackEnemyChosen == 2)
            {
                animatorEnemyMonstrz = enemyMonster.monsterModel.GetComponent<Animator>();
                animatorEnemyMonstrz.SetTrigger("BlockAction");
            }
            else
            {
                animatorEnemyMonstrz = enemyMonster.monsterModel.GetComponent<Animator>();
                animatorEnemyMonstrz.SetTrigger("GrabAction");
            }
           
            SetNewStatsFight(moveType, attackEnemyChosen);
       

        }
    }



    private void SetNewStatsFight(string moveType, int attackEnemyChosen)
    {


        int healthToRemovePlayer=7; //remove to the player

        int healthToRemoveEnemy=7; //remove to the enemy

        if (moveType == "attack") {

            
            if (attackEnemyChosen == 0) //attack
            {
              
            }
            else if (attackEnemyChosen == 1) //dodge
            {
                healthToRemoveEnemy = 9;
                healthToRemovePlayer = 0;
            }
            else if (attackEnemyChosen == 2) //block
            {
                healthToRemoveEnemy = 4;
                healthToRemovePlayer = 0;
            }
            else //grab
            {
              
            }

        }
        else if (moveType == "dodge")
        {
            healthToRemoveEnemy = 0;

            if (attackEnemyChosen == 0) //attack
            {
                healthToRemovePlayer = 9;
            }
            else if (attackEnemyChosen == 1) //dodge
            {
                healthToRemovePlayer = 0;
            }
            else if (attackEnemyChosen == 2) //block
            {
                healthToRemovePlayer = 0;
            }
            else //grab
            {
                healthToRemovePlayer = 4;
            }

        }
        else if (moveType == "block")
        {
            healthToRemoveEnemy = 0;

            if (attackEnemyChosen == 0) //attack
            {
                healthToRemovePlayer = 4;
            }
            else if (attackEnemyChosen == 1) //dodge
            {
                healthToRemovePlayer = 0;
            }
            else if (attackEnemyChosen == 2) //block
            {
                healthToRemovePlayer = 0;
            }
            else //grab
            {
                healthToRemovePlayer = 9;
            }


        }
        else //grab
        {



            if (attackEnemyChosen == 0) //attack
            {
                
            }
            else if (attackEnemyChosen == 1) //dodge
            {
                healthToRemoveEnemy = 4;
                healthToRemovePlayer = 0;
            }
            else if (attackEnemyChosen == 2) //block
            {
                healthToRemoveEnemy = 9;
                healthToRemovePlayer = 0;
            }
            else //grab
            {

            }


        }


        enemyMonster.monsterStats.health -= healthToRemoveEnemy;
        currentMonster.monsterStats.health -= healthToRemovePlayer;

        if (enemyMonster.monsterStats.health <= 0)
        {
            fightComplete = true;
            playerWin = true;
        }
        else if (currentMonster.monsterStats.health <= 0)
        {
            fightComplete = true;
            playerWin = false;
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
                animatorMonstrz.SetTrigger("MonstrzSelected");
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
                animatorMonstrz.SetTrigger("MonstrzSelected");
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

    private void SelectEnemyType(int type1,int type2,int typeWrong)
    {
        int enemyType = typeWrong;
        
        while (enemyType == typeWrong)
        {
            enemyType = Random.Range(0, 3);
        }
       


        selectMonsters[enemyType].monsterModel = Instantiate(selectMonsters[enemyType].gameObject, fightPositions[1].transform.position, selectMonsters[enemyType].spawnPoint[0].rotation) as GameObject;

        selectMonsters[enemyType].CreateMonster();

        selectMonsters[enemyType].monsterModel.transform.SetParent(target.transform);

        selectMonsters[enemyType].monsterModel.transform.Rotate(0, 90, 0);
        enemyMonster = selectMonsters[enemyType];

        if (enemyMonster.monsterStats.health < 30)
        {
            enemyMonster.monsterStats.health = 30;
        }

        enemySlider.maxValue = enemySlider.value = enemyMonster.monsterStats.health;
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

    public void TargetDetectedSelectButton(bool found)
    {
      

        selectButton.SetActive(found);
    }
}
