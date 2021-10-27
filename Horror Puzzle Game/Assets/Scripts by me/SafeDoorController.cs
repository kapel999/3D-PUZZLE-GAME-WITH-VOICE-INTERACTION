using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for the mechanic behind the safe's door.
/// Once called it will check if player has a key within their backpack 
/// and if yes it will open the safe, disable the sphere that allows to 
/// open it, gets rid of the key from inventory and enables the interaction
/// sphere for the statue inside of the safe.
/// </summary>
public class SafeDoorController : MonoBehaviour
{
    private AudioSource audioSource;

    public GameObject backpack;
    public GameObject safeDoorSphere;
    public GameObject statueSphere;
    public GameObject keyInventoryNumber;
    public GameObject keyInventoryImage;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void openSafe()
    {
        if(backpack.GetComponent<BackpackController>().keysInBackpack == 1)
        {
            audioSource.Play();
            safeDoorSphere.SetActive(false);
            keyInventoryImage.SetActive(false);
            keyInventoryNumber.SetActive(false);
            gameObject.GetComponent<Animator>().Play("Base Layer.SafeDoorAnimation");
            statueSphere.SetActive(true);
        }
    }
}
