using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceInteractionFirstLevel : MonoBehaviour
{
    private AudioSource audioSource;
    // Will store the words spoken by the player
    private KeywordRecognizer keywordRecognizer;
    // Will store entries that will contain of a string variable (word) and an action that will be represented by the string variable
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    // Will store the animations
    private Animator animator;

    // Will store the game object of doors that will be opened by the voice interaction
    public GameObject doorsToNextLevel;
    // Will store the game object of interactable sphere that allows to trigger interactions on button press if player is within range
    public GameObject interactable;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //Allows for the animator to access and store animations of the connected game object
        animator = doorsToNextLevel.GetComponent<Animator> ();

        // This will add to the dictionary the string and the name of function that is attached to that string
        actions.Add("2137", PinLevelOne);

        // This will allow the keywordRecognizer to look for words that have been added to the actions dictionary
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.High);
        // This line of code will run the "SpeechThatWasRecognized" method each time the player speaks
        keywordRecognizer.OnPhraseRecognized += SpeechThatWasRecognized;
        
    }
    /// <summary>
    /// Runs everything inside of it as many times as the game refreshes every second. 
    /// </summary>
    private void Update()
    {
        // Checks if the voice recognition system is all ready on
        if (!interactable.GetComponent<Interactable>().isInRange && keywordRecognizer.IsRunning)
        {
            // Stops the voice recognizer from working
            keywordRecognizer.Stop();
        }
        
    }
    /// <summary>
    /// Method that will turn the voice recognizer on so the player can be heard
    /// </summary>
    public void pressToTalk()
    {
        // Turns on the keyword recognizer so it can hear the player
        keywordRecognizer.Start();

    }
    /// <summary>
    /// This function will only be run if the player is heard saying words from the dictionary. 
    /// If the thin the player said is within the dictionary then the phrase will be shown in debugger and the action assigned to the 
    /// word will be performed.
    /// </summary>
    /// <param name="speech">stores what the player have said to the microphone</param>
    private void SpeechThatWasRecognized (PhraseRecognizedEventArgs speech)
    {
        // This line takes the player speech as a key and invokes an action assigned to the word if one exists
        actions[speech.text].Invoke();
    }
    /// <summary>
    /// This method was created to test if the voice recognition is working correctly. 
    /// </summary>
    private void PinLevelOne()
    {
        // Plays the animation specified 
        animator.Play("Base Layer.DoorToNextRoom");
        audioSource.Play();
    }
}
