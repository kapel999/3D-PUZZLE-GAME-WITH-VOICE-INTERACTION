using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SecondLevelVoiceInteraction : MonoBehaviour
{
    private AudioSource audioSource;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    // Variable storing if the object is colliding with the maze walls
    private bool isCollidingMazeWall;
    // Variable storing number representing which move caused the collision
    private int whichAction;
    // Variable storing if the maze has been completed or not
    private bool isMazeCompleted;
    private bool chestWasOpened;

    public GameObject chest;
    public Animator animatorOfMazeText;
    public GameObject interactable;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = chest.GetComponent<AudioSource>();

        actions.Add("Up", MoveUp);
        actions.Add("Down", MoveDown);
        actions.Add("Left", MoveLeft);
        actions.Add("Right", MoveRight);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += SpeechThatWasRecognized;
    }

    // Update is called once per frame
    void Update()
    {
        if (keywordRecognizer.IsRunning == true)
        {
            if (interactable.GetComponent<Interactable>().isInRange == false)
            {
                keywordRecognizer.Stop();

            }
        }
        // Runs if the veriable is set to true
        if (isCollidingMazeWall)
        {
            // Sets the variable back to false
            isCollidingMazeWall = false;
            // Depending which move caused the collision opposite move will be called to put player back in correct position
            if (whichAction == 1)
            {
                MoveDown();
            }
            else if (whichAction == 2)
            {
                MoveUp();
            }
            else if (whichAction == 3)
            {
                MoveRight();
            }
            else if (whichAction == 4)
            {
                MoveLeft();
            }
            // Plays the animation and always starts from the first state so the animation is reset before playing again
            animatorOfMazeText.GetComponent<Animator>().Play("Base Layer.AppearingMazeWarning", -1, 0);
        }
        if (isMazeCompleted && !chestWasOpened)
        {
            chestWasOpened = true;
            chest.GetComponent<Animator>().Play("Base Layer.OpeningChestsCap");
            audioSource.Play();
        }

    }

    private void SpeechThatWasRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void pressToTalk()
    {
        keywordRecognizer.Start();
    }

    /// <summary>
    /// Checks if the game object touches MazeWalls or the EndPointOfMaze and if it does set the apropriate 
    /// vairables to true
    /// </summary>
    /// <param name="collision">Object collider</param>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("MazeWalls"))
        {
            isCollidingMazeWall = true;
        }
        if (collision.gameObject.CompareTag("EndPointOfMaze"))
        {
            isMazeCompleted = true;
        }
    }

    /// <summary>
    /// Moves the game object up in the maze when called 
    /// </summary>
    private void MoveUp()
    {
        transform.Translate(0, 0.35f, 0);
        whichAction = 1;
    }

    /// <summary>
    /// Moves the game object down in the maze when called 
    /// </summary>
    private void MoveDown()
    {
        transform.Translate(0, -0.35f, 0);
        whichAction = 2;
    }

    /// <summary>
    /// Moves the game object left in the maze when called 
    /// </summary>
    private void MoveLeft()
    {
        transform.Translate(0.35f, 0, 0);
        whichAction = 3;
    }

    /// <summary>
    /// Moves the game object right in the maze when called 
    /// </summary>
    private void MoveRight()
    {
        transform.Translate(-0.35f, 0, 0);
        whichAction = 4;
    }
}
