using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SofaPillowController : MonoBehaviour
{
    private AudioSource audioSource;

    // Variable that will allow to access the game object animator
    public Animator animator;
    // Variable that will store if the pillow was moved or not
    public bool isMoved;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    /// <summary>
    /// Method that will perform the pillow animation if it has not been perfomed all ready
    /// </summary>
    public void movePillow()
    {
        if (!isMoved)
        {
            isMoved = true;
            audioSource.Play();
            animator.Play("Base Layer.RightPillowSofa");
        }
    }
}
