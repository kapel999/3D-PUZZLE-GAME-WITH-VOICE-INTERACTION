using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will is responsible displaying the "Well Done" message.
/// Once called by player colliding with the invisible object it will
/// set active all the different objects that are hidden so player will
/// have a small suprise.
/// </summary>
public class wellDoneScript : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isInRange;

    public GameObject hiddenObjects;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            hiddenObjects.SetActive(true);
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            isInRange = true;
        }
    }
}
