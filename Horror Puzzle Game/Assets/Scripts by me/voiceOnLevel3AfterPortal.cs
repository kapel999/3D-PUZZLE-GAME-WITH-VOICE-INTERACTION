using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// This script will check if player walked through the portal
/// if yes the collision will set the boolean variable 
/// to true. Then the script allows other scripts to access it through 
/// the getter function.
/// </summary>
public class voiceOnLevel3AfterPortal : MonoBehaviour
{
    private bool hasPlayerWentThroughPortal;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            hasPlayerWentThroughPortal = true;
        }
    }

    public bool getHasPlayerWentThroughPortal()
    {
        return hasPlayerWentThroughPortal;
    }
}
