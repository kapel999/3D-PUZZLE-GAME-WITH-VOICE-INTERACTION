using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for picking up the key mechanic.
/// Once the method is called it will check if the player has a 
/// backpack to be able to pick up and store objects and then 
/// activate UI element for key and it will add one to the amount
/// of keys in the backpack.
/// </summary>
public class KeyController : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject backpack;
    public GameObject uiKey;
    public GameObject uiKeyCounter;
    public GameObject keyInteractionSphere;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = audioSource.GetComponent<AudioSource>();
    }

    public void pickUpKey()
    {
        if (backpack.GetComponent<BackpackController>().hasBackpack == true)
        {
            audioSource.Play();
            uiKey.SetActive(true);
            uiKeyCounter.SetActive(true);
            backpack.GetComponent<BackpackController>().keysInBackpack = 1;
            keyInteractionSphere.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
