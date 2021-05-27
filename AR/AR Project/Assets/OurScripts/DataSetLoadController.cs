using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class DataSetLoadController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var databaseLoader = DatabaseLoadARController.Instance;
        databaseLoader.AddExternalDatasetSearchDir(Application.dataPath + "/Raw/Vuforia/");
    }

    // Update is called once per frame
    void Update()
    {

    }
}