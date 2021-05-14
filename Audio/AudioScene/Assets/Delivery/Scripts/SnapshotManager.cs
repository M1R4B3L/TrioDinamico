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
             FactorySnap.TransitionTo(0.5f);
                break;

            case "In_House":
                HouseSnap.TransitionTo(0.5f);
                break;

            case "In_Forest":
                ForestSnap.TransitionTo(0.5f);
                break;

            case "In_Water":
                WaterSnap.TransitionTo(0.5f);
                break;

            default:
                General.TransitionTo(0.5f);
                break;
        }

        Debug.Log(tag);
    }

    private void OnTriggerExit(Collider other)
    {
      General.TransitionTo(0.1f);
    }
  
}
