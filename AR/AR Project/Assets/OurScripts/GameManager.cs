using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Tap to play Screen

    public Text[] Texts;

    private Touch touch_info;

    private Scene currentScene;


    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
       // Debug.Log("Current Scene Name ->"+ currentScene.name);
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
}
