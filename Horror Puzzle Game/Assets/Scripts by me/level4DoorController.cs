using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// This script is resposible for the mechanic of unlocking the door to next level
/// if the player provides the correct password. It will also trigger on collision 
/// the voice recognition and it will inform the player that they are heard by the 
/// game by changing the writting above the door. If the correct words are provided 
/// in the correct order the animation of opening the door will play and allow the 
/// player to enter the next room.
/// </summary>
public class level4DoorController : MonoBehaviour
{
    private AudioSource audioSource;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private bool isInRange;

    public GameObject door;
    public GameObject comeCloserMessage;
    public GameObject closeEnoughMessage;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        actions.Add("fire skull forest ghost", openDoor);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += SpeechThatWasRecognized;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && !keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Start();
        }
        else if (keywordRecognizer.IsRunning && !isInRange)
        {
            keywordRecognizer.Stop();
        }
    }

    private void SpeechThatWasRecognized(PhraseRecognizedEventArgs speech)
    {
        actions[speech.text].Invoke();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            comeCloserMessage.SetActive(false);
            closeEnoughMessage.SetActive(true);
            isInRange = true;
        }
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            comeCloserMessage.SetActive(true);
            closeEnoughMessage.SetActive(false);
            isInRange = false;
        }
    }

    private void openDoor()
    {
        audioSource.Play();
        door.GetComponent<Animator>().Play("Base Layer.level4DoorAnimation");
    }
}
