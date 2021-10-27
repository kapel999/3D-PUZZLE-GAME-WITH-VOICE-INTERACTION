using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeDoorController : MonoBehaviour
{
    // Variable that will store if the doors are open or not
    private bool isOpen;
    private AudioSource audioSource;

    public GameObject leftDoor;
    public GameObject rightDoor;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Method that will play animations for both of the doors 
    /// </summary>
    public void OpenDoor()
    {
        // Only runs if boolean value is false
        if (!isOpen)
        {
            // The value is changed to true
            isOpen = true;
            // Both animations are played for the right and the left door
            leftDoor.GetComponent<Animator>().Play("Base Layer.WardrobeLeftDoor");
            rightDoor.GetComponent<Animator>().Play("Base Layer.WardrobeRightDoor");
            audioSource.Play();
        }
    }
}
