using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnapshotManager : MonoBehaviour
{
    public AudioMixerSnapshot General;
    public AudioMixerSnapshot FactorySnap;
    public AudioMixerSnapshot HouseSnap;
    public AudioMixerSnapshot ForestSnap;
    public AudioMixerSnapshot WaterSnap;
    
    private void OnTriggerEnter(Collider other)
    {
        tag = other.gameObject.tag;



        switch (tag)
        {

            case "In_Factory":
                FactorySnap.TransitionTo(1f);
                break;

            case "In_House":
                HouseSnap.TransitionTo(1f);
                break;

            case "In_Forest":
                ForestSnap.TransitionTo(1f);
                break;

            case "In_Water":
                WaterSnap.TransitionTo(1f);
                break;

            default:
                General.TransitionTo(1f);
                break;





        }

      
    }

    private void OnTriggerExit(Collider other)
    {
        General.TransitionTo(1f);
    }

   
}
