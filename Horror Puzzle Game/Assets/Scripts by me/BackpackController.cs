using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will be responsible for picking up the back pack mechanic.
/// Once the methd will be called the backpack will disappear the inventory UI
/// will be visable and possible items that could be in the back pack will be 
/// set to 0 as backpack is empty.
/// </summary>
public class BackpackController : MonoBehaviour
{
    private AudioSource audioSource;

    public GameObject backpack;
    public GameObject inventoryUI;
    public bool hasBackpack;
    public int statuesInBackpack;
    public int keysInBackpack;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void pickUpBackPack()
    {
        audioSource.Play();
        backpack.SetActive(false);
        inventoryUI.SetActive(true);
        hasBackpack = true;
        statuesInBackpack = 0;
        keysInBackpack = 0;
    }
}
