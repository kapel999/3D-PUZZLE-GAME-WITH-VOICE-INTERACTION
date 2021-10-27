using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will check if player walked out of the cage
/// if yes the collision will perform setting the boolean variable 
/// to true. Then the script allows other scripts to access it through 
/// the getter function.
/// </summary>
public class TurnOffCageVoiceInteraction : MonoBehaviour
{
    private bool turnOffCageVoiceInter;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            turnOffCageVoiceInter = true;
        }
    }

    public bool getTurnOffCageVoiceInter()
    {
        return turnOffCageVoiceInter;
    }
}
