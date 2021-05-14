using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIteration : MonoBehaviour
{
    public AudioClip[] iterations;
  

    private AudioSource aSource;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cube")
        {
            aSource.PlayOneShot(iterations[0]);
        }
        else if(other.name == "AlarmTrigger")
        {
            aSource.PlayOneShot(iterations[1]);
        }
        else
            return;


    }

}
