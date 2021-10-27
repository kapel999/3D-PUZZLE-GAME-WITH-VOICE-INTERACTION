using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script allows for performing all the animations behind the axe puzzle as well as 
/// changes the interactable spheres to the one that will allow player to pick up the statue
/// </summary>
public class AxePuzzleController : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animatorAxeLeft;
    private Animator animatorAxeRight;
    private Animator animatorWallLeft;
    private Animator animatorWallRight;

    public GameObject axeLeft;
    public GameObject axeRight;
    public GameObject wallAxeLeft;
    public GameObject wallAxeRight;
    public GameObject interactableSphereForStatue;
    public GameObject interactableSphereForAnimations;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AxeInteraction()
    {
        audioSource.Play();

        // Sets the animator variables to object animators
        animatorAxeLeft = axeLeft.GetComponent<Animator>();
        animatorAxeRight = axeRight.GetComponent<Animator>();
        animatorWallLeft = wallAxeLeft.GetComponent<Animator>();
        animatorWallRight = wallAxeRight.GetComponent<Animator>();

        // Plays animation for the axe
        animatorAxeLeft.Play("Base Layer.AxeLeft");
        animatorAxeRight.Play("Base Layer.AxeRight");
                
        // Plays animation for the wall
        animatorWallLeft.Play("Base Layer.AxeWallLeft");
        animatorWallRight.Play("Base Layer.AxeWallRight");

        // Changes the interactable spheres
        interactableSphereForAnimations.SetActive(false);
        interactableSphereForStatue.SetActive(true);
    }
}
