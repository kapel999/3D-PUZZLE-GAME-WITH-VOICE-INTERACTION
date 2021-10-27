using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    private AudioSource audioSource;
    // Will store an integer represeting which way the dragon is facing
    private int positionNumber;

    // Will store the connection to the object of table which contains everything voice recogntion related
    public GameObject coffeTable;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// This method rotates the object and each time it rotates it adds one to the counter. 
    /// Then once the dragon is in the correct position the table that allows for voice interaction will be visable and usable to the player.
    /// </summary>
    public void RotateTheObject()
    {
        audioSource.Play();
        // Rotates the object by 90 degrees in the y axis
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);
        // Increases the number by one
        positionNumber++;
        // Checks if the object is in it's third and correct position
        if (positionNumber == 3)
        {
            // Activates the game object and all it's childer so the player can use it
            coffeTable.SetActive(true);
            // Sets the positon to -1 so on the next turn it will restart to zero
            positionNumber = -1;
        }
        else
        {
            // Diactivates the game object and all it's childer so the player cannot use it if the object isn't in correct position
            coffeTable.SetActive(false);
        }
    }
}
