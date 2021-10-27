using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script is reponsible for the mechanic behind picking up the different statues.
/// When called the script will check if player has a backpack, if so it will check if 
/// inside of the backpack the player has any statues. If not the game object of the 
/// statue will dissapear and the ui elements will appear. If player had all ready
/// a statue the script will dissapear the statue and increment the number of statues
/// in the back pack. Then the ui number will be updated accordingly. In both scenarios 
/// the interaction sphere is disabled at the end.
/// </summary>
public class StatueController : MonoBehaviour
{
    public GameObject backpack;
    public GameObject uiStatue;
    public GameObject uiStatueCounter;
    public GameObject statueInteractionSphere;
    public AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = audioSource.GetComponent<AudioSource>();
    }

    public void pickUpStatue()
    {
        if (backpack.GetComponent<BackpackController>().hasBackpack == true)
        {
            audioSource.Play();
            if (!uiStatue.activeSelf)
            {
                gameObject.SetActive(false);
                uiStatue.SetActive(true);
                uiStatueCounter.SetActive(true);
                backpack.GetComponent<BackpackController>().statuesInBackpack = 1;
                statueInteractionSphere.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
                backpack.GetComponent<BackpackController>().statuesInBackpack++;
                uiStatueCounter.GetComponent<TextMeshProUGUI>().text = backpack.GetComponent<BackpackController>().statuesInBackpack.ToString();
                statueInteractionSphere.SetActive(false);
            }
        }
    }
}
