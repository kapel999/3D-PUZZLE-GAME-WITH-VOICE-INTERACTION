using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for the moving of the painting mechanic.
/// Once method is called the sphere that allows for activation of this 
/// mechanic will be disabled, animation will play and then the interaction
/// sphere for safe mechanic will be activated. 
/// </summary>
public class PaintingSafeController : MonoBehaviour
{
    private AudioSource audioSource;

    public GameObject paintingSphere;
    public GameObject safeSphere;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void movePainting()
    {
        audioSource.Play();
        paintingSphere.SetActive(false);
        gameObject.GetComponent<Animator>().Play("Base Layer.SafePaintingAnimation");
        safeSphere.SetActive(true);
    }
}
