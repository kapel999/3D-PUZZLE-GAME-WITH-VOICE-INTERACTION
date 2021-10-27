using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteController : MonoBehaviour
{
    private AudioSource audioSource;
    // Variable that will allow to access the game object animator
    private Animator animator;
    // Variable that will store the link between this script and the TV game object 
    public GameObject tv;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // saves the animator of the tv object as the animator variable 
        animator = tv.GetComponent<Animator> ();
    }

    /// <summary>
    /// Method which will play the animation of the TV being turned on
    /// </summary>
    public void UseRemote()
    {
        audioSource.Play();
        animator.Play("Base Layer.TvOn");
    }
}
