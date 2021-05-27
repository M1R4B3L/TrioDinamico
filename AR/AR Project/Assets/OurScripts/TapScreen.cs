using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TapScreen : MonoBehaviour
{

    public Text tapText;
    private Touch touch_info;

   
    void Start()
    {
        
    }

   
    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            touch_info = Input.GetTouch(0);
            Debug.Log("Screen Touched");
            tapText.color = Color.red;

            if (touch_info.phase == TouchPhase.Ended)
            {

                SceneManager.LoadScene("SelectionScreen", LoadSceneMode.Single);


            }
        }
        else
        {
            tapText.color = Color.black;
        }



    }
}
