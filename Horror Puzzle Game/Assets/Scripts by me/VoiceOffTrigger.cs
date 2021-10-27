using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will check if the player collided with the object that this script is 
/// applied to and perform actions accordingly. This will be mainly used with the 
/// mechanic of the player going through one of the gates to hidden or main level.
/// </summary>
public class VoiceOffTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    // Stores if the player is still in the room of choice
    private bool isInRangeVoiceOff;
    // Stores animator of the wall game object
    private Animator wallAnimator;

    // Stores the game object of the wall this script is corresponding with
    public GameObject wallPassThrough;
    // Stores game object of the secret writting appearing within the choice room
    public GameObject hiddenWayWritting;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Sets the animator to the animator of the wall game object
        wallAnimator = wallPassThrough.GetComponent<Animator>();
    }

    /// <summary>
    /// This function will check if the object collider collides with the player and 
    /// if so it will perform actions accordingly.
    /// </summary>
    /// <param name="collision">Collider of the object</param>
    private void OnTriggerEnter(Collider collision)
    {
        // Checks if the game object collides with the player object and if the player all ready triggered it
        if (collision.gameObject.CompareTag("Player") && isInRangeVoiceOff == false)
        {
            isInRangeVoiceOff = true;
            wallAnimator.Play("Base Layer.WallCloseAnimation");
            audioSource.Play();
            hiddenWayWritting.SetActive(true);
        }
    }

    public bool getIsInRangeVoiceOff()
    {
        return isInRangeVoiceOff;
    }
}
