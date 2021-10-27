using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// This script will perform the mechanics behind the cage system in the game.
/// </summary>
public class VoiceCageScript : MonoBehaviour
{
    private AudioSource audioSource;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    // Stores number which represents how many times player said specified phrase
    private int speechCounter;
    // Stores animator of the one of the walls of the cage
    private Animator openingCageAnimator;

    // Stores true or false depending if the player object is colliding with collider of the cage trigger
    public bool isInRange;
    // Stores link to the cage parts that are only visible when the cage becomes active
    public GameObject invisiblePartsOfCage;
    // Stores link to the wall that will dissapear once the player fullfills the task given
    public GameObject disappearingWall;
    // Stores link to invisible surface that acts as a trigger for turning off speech recognition and turning back on the wall
    public GameObject triggerOffForVoiceRecognition;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        openingCageAnimator = invisiblePartsOfCage.GetComponentInChildren<Animator>();

        actions.Add("I will obey the rules", increaseCounter);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += SpeechThatWasRecognized;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            // Checks if the spech recognition isn't all ready on
            if (!keywordRecognizer.IsRunning)
            {
                keywordRecognizer.Start();
            }
            // Checks the speech counter for specified value allowing control over how many times player has to repeat the phrase
            if (speechCounter == 5)
            {
                disappearingWall.SetActive(false);
                // Allows to access the writting object within the cage that is the child object of the cage
                invisiblePartsOfCage.transform.GetChild(1).gameObject.SetActive(false);
                openingCageAnimator.Play("Base Layer.OpeningTheCage");
                audioSource.Play();

            }
        }
        // Checks with the use of a getter if the player collided with the connected invisible object and if the speech recognition is all ready running
        if (triggerOffForVoiceRecognition.GetComponent<TurnOffCageVoiceInteraction>().getTurnOffCageVoiceInter() == true && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
            isInRange = false;
        }
    }

    /// <summary>
    /// Checks if the collider of the object collided with the player object.
    /// If so it activates the cage.
    /// </summary>
    /// <param name="collision">Collider of the object</param>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            invisiblePartsOfCage.SetActive(true);
        }
    }

    /// <summary>
    /// Translates player speech into text and checks if the speech matches 
    /// any of the string entries within the dictionary. If so it will invoke the 
    /// action assigned to the keyword or keyphrase.
    /// </summary>
    /// <param name="speech">Stores what player said to the microphone</param>
    private void SpeechThatWasRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    /// <summary>
    /// Increases the counter by one as long as the number stored is below ten.
    /// </summary>
    private void increaseCounter()
    {
        speechCounter++;
    }
}
