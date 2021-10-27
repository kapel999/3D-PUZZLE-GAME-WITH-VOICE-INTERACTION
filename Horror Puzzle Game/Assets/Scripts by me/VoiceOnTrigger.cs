using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// This script will allow for the voice interaction in the room where player is given a choice to go straight to last level
/// or go through the hidden level
/// </summary>
public class VoiceOnTrigger : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    // Stores animator of the wall which will move if player decides to pick the secret level
    private Animator wrongPathWallAnimator;
    // Stores animator of the wall which will move if player decides to pick the last level
    private Animator goodPathWallAnimator;


    public bool isInRange;
    // Stores link to the invisible object which is hidden behind the wall to the hidden level
    public GameObject badWayTrigger;
    // Stores link to the invisible object which is hidden behind the wall to the last level
    public GameObject goodWayTrigger;
    // Stores link to the wall that will move when hidden level is chosen
    public GameObject badWayWallPassThrough;
    // Stores link to the wall that will move when last level is chosen
    public GameObject goodWayWallPassThrough;
    // Stores link to invisible object under the receiver portal
    public GameObject checkIfPlayerWentThroughPortal;


    // Start is called before the first frame update
    void Start()
    {
        // Sets the animators
        goodPathWallAnimator = goodWayWallPassThrough.GetComponent<Animator>();
        wrongPathWallAnimator = badWayWallPassThrough.GetComponent<Animator>();

        // Adding words that will trigger actions when said to the dictionary
        actions.Add("I pick left", mainLevelOpen);
        actions.Add("I pick right", extraLevelOpen);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Medium);
        keywordRecognizer.OnPhraseRecognized += SpeechThatWasRecognized;
    }

    // Update is called once per frame
    void Update()
    {
        // If player collides with the object or the speech recognition is all ready on
        if (isInRange && !keywordRecognizer.IsRunning)
        {
            // Turns on the speech recognition 
            keywordRecognizer.Start();
        }
        // Uses getters of the invisible objects behind walls to check if player has entered one of the two rooms and then
        // checks if speech recogntion is all ready on
        if ((badWayTrigger.GetComponent<VoiceOffTrigger>().getIsInRangeVoiceOff() == true || goodWayTrigger.GetComponent<VoiceOffTrigger>().getIsInRangeVoiceOff() == true) 
            && keywordRecognizer.IsRunning)
        {
            // Turns off the speech recognition
            keywordRecognizer.Stop();
        }
        // Uses getter of the invisible object under receiver portal to check if player walked through it and it also checks if speech recognition is on
        if (checkIfPlayerWentThroughPortal.GetComponent<voiceOnLevel3AfterPortal>().getHasPlayerWentThroughPortal() == true && !keywordRecognizer.IsRunning)
        {
            // Turns on the speech recognition
            keywordRecognizer.Start();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private void SpeechThatWasRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    /// <summary>
    /// Plays the animation which opens the path to the main level
    /// </summary>
    private void mainLevelOpen()
    {    
        goodPathWallAnimator.Play("Base Layer.CorrectWayWallAnimation");
        goodWayWallPassThrough.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// Plays the animation which opens the path to the extra level only if the player has not went through the portal all ready
    /// </summary>
    private void extraLevelOpen()
    {
        if (checkIfPlayerWentThroughPortal.GetComponent<voiceOnLevel3AfterPortal>().getHasPlayerWentThroughPortal() == false)
        {
            wrongPathWallAnimator.Play("Base Layer.BadWayWallAnimation");
            badWayWallPassThrough.GetComponent<AudioSource>().Play();
        }
           
    }
}
